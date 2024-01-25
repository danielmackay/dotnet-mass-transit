using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Consumer.Consumers;

public class HelloWorldConsumer : IConsumer<Contracts.HelloWorld>
{
    private readonly ILogger<HelloWorldConsumer> _logger;

    public HelloWorldConsumer(ILogger<HelloWorldConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<Contracts.HelloWorld> context)
    {
        _logger.LogInformation("Received Text: {Value}", context.Message.Value);

        return Task.CompletedTask;
    }
}
