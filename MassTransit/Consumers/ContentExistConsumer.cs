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
    public class ContentExistConsumer : IConsumer<ContentExistEvent>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<ContentExistConsumer>();

        private readonly CommonService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContentExistConsumer(CommonService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Consume
        /// </summary>
        public async Task Consume(ConsumeContext<ContentExistEvent> context)
        {
            var message = context.Message;

            var isExists = await _dataService.ContentExistAsync(message.ContentId);

            var response = new ContentExistResponse() 
            { 
                ContentId = message.ContentId,
                IsExists = isExists
            };

            await context.RespondAsync(response);
        }
    }
}
