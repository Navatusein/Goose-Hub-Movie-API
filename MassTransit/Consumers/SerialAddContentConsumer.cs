using MassTransit;
using MassTransit.Testing;
using MovieApi.MassTransit.Events;
using MovieApi.Models;
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

        /// <summary>
        /// Constructor
        /// </summary>
        public SerialAddContentConsumer(SerialService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Consume
        /// </summary>
        public async Task Consume(ConsumeContext<SerialAddContentEvent> context)
        {
            var message = context.Message;

            await _dataService.AddEpisodeContentAsync(message.EpisodeId, message.Content);
        }
    }
}
