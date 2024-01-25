using MassTransit;

namespace Consumer.Consumers;

public class HelloWorldConsumerDefinition :
    ConsumerDefinition<HelloWorldConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<HelloWorldConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}
