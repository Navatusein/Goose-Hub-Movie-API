using MassTransit;
using MassTransit.Testing;
using MovieApi.MassTransit.Events;
using MovieApi.Models;
using MovieApi.Service;
using MovieApi.Services.DataServices;

namespace MovieApi.MassTransit.Consumers
{
    /// <summary>
    /// Consumer for SerialAddContentEvent
    /// </summary>
    public class SerialAddContentConsumer : IConsumer<SerialAddContentEvent>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<SerialAddContentConsumer>();

        private readonly SerialService _dataService;
        private readonly MinioService _minioService;

        /// <summary>
        /// Constructor
        /// </summary>
        public SerialAddContentConsumer(SerialService dataService, MinioService minioService)
        {
            _dataService = dataService;
            _minioService = minioService;
        }

        /// <summary>
        /// Consume
        /// </summary>
        public async Task Consume(ConsumeContext<SerialAddContentEvent> context)
        {
            var message = context.Message;

            var filePath = $"{message.ContentFileName}/main.m3u8";

            var fileExists = await _minioService.GenerateHlsDescriptorAsync(filePath, message.Quality);

            if (!fileExists)
                await _dataService.AddEpisodeContentAsync(message.EpisodeId, filePath);
        }
    }
}
