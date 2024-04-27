using MassTransit;
using MovieApi.MassTransit.Events;
using MovieApi.MassTransit.Responses;
using MovieApi.Models;
using MovieApi.Services.DataServices;

namespace MovieApi.MassTransit.Consumers
{
    /// <summary>
    /// Consumer for ContentExistEvent
    /// </summary>
    public class EpisodeExistConsumer : IConsumer<EpisodeExistEvent>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<EpisodeExistConsumer>();

        private readonly PreviewService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public EpisodeExistConsumer(PreviewService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Consume
        /// </summary>
        public async Task Consume(ConsumeContext<EpisodeExistEvent> context)
        {
            var message = context.Message;

            var isExists = await _dataService.EpisodeExistAsync(message.EpisodeId);

            var response = new EpisodeExistResponse() 
            {
                EpisodeId = message.EpisodeId,
                IsExists = isExists
            };

            await context.RespondAsync(response);
        }
    }
}
