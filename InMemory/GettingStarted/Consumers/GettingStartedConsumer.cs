using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GettingStarted.Consumers
{
    public class GettingStartedConsumer : IConsumer<Contracts.GettingStarted>
    {
        private readonly ILogger<GettingStartedConsumer> _logger;

        public GettingStartedConsumer(ILogger<GettingStartedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Contracts.GettingStarted> context)
        {
            _logger.LogInformation("Received Text: {Value}", context.Message.Value);

            return Task.CompletedTask;
        }
    }
}
