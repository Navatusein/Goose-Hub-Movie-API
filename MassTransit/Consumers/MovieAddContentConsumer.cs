using MassTransit;
using MovieApi.Controllers;
using MovieApi.MassTransit.Events;
using MovieApi.Models;
using MovieApi.Services.DataServices;

namespace MovieApi.MassTransit.Consumers
{
    /// <summary>
    /// Consumer for MovieAddContentEvent
    /// </summary>
    public class MovieAddContentConsumer : IConsumer<MovieAddContentEvent>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<MovieAddContentConsumer>();

        private readonly MovieService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public MovieAddContentConsumer(MovieService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Consume
        /// </summary>
        public async Task Consume(ConsumeContext<MovieAddContentEvent> context)
        {
            var message = context.Message;

            await _dataService.AddContentAsync(message.MovieId, message.Content);
        }
    }
}
