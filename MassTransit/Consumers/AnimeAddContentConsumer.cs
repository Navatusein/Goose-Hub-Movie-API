using MassTransit;
using MovieApi.Controllers;
using MovieApi.MassTransit.Events;
using MovieApi.Models;
using MovieApi.Service;
using MovieApi.Services.DataServices;

namespace MovieApi.MassTransit.Consumers
{
    /// <summary>
    /// Consumer for AnimeAddContentEvent
    /// </summary>
    public class AnimeAddContentConsumer : IConsumer<AnimeAddContentEvent>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<AnimeAddContentConsumer>();

        private readonly AnimeService _dataService;
        private readonly MinioService _minioService;

        /// <summary>
        /// Constructor
        /// </summary>
        public AnimeAddContentConsumer(AnimeService dataService, MinioService minioService)
        {
            _dataService = dataService;
            _minioService = minioService;
        }

        /// <summary>
        /// Consume
        /// </summary>
        public async Task Consume(ConsumeContext<AnimeAddContentEvent> context)
        {
            var message = context.Message;

            var filePath = $"{message.ContentFileName}/main.m3u8";

            var fileExists = await _minioService.GenerateHlsDescriptorAsync(filePath, message.Quality);

            if (fileExists)
            {
                if (message.IsEpisode)
                {
                    await _dataService.AddEpisodeContentAsync(message.ContentId, filePath);
                }
                else
                {
                    await _dataService.AddContentAsync(message.ContentId, filePath);
                }
            }
        }
    }
}
