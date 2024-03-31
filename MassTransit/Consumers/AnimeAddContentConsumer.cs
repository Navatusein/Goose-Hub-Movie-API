using MassTransit;
using MovieApi.Controllers;
using MovieApi.MassTransit.Events;
using MovieApi.Models;
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

        /// <summary>
        /// Constructor
        /// </summary>
        public AnimeAddContentConsumer(AnimeService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Consume
        /// </summary>
        public async Task Consume(ConsumeContext<AnimeAddContentEvent> context)
        {
            var message = context.Message;
          
            if (message.IsEpisode)
            {
                await _dataService.AddEpisodeContentAsync(message.Id, message.Content);
            }
            else
            {
                await _dataService.AddContentAsync(message.Id, message.Content);
            }
        }
    }
}
