using MassTransit;
using MovieApi.MassTransit.Events;
using MovieApi.Models;
using MovieApi.Service;
using MovieApi.Services.DataServices;
using System.Text;

namespace MovieApi.MassTransit.Consumers
{
    /// <summary>
    /// Consumer for MovieAddContentEvent
    /// </summary>
    public class MovieAddContentConsumer : IConsumer<MovieAddContentEvent>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<MovieAddContentConsumer>();

        private readonly MovieService _dataService;
        private readonly MinioService _minioService;

        /// <summary>
        /// Constructor
        /// </summary>
        public MovieAddContentConsumer(MovieService dataService, MinioService minioService)
        {
            _dataService = dataService;
            _minioService = minioService;
        }

        /// <summary>
        /// Consume
        /// </summary>
        public async Task Consume(ConsumeContext<MovieAddContentEvent> context)
        {
            var message = context.Message;
            var filePath = $"{message.ContentFileName}/main.m3u8";

            var fileExists = await _minioService.GenerateHlsDescriptorAsync(filePath, message.Quality);
            
            if (!fileExists)
                await _dataService.AddContentAsync(message.MovieId, filePath);
        }
    }
}
