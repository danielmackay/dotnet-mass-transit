using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Consumer.Consumers;

public class CheckOrderStatusConsumer : IConsumer<OrderStatusRequest>
{
    private readonly ILogger<CheckOrderStatusConsumer> _logger;

    public CheckOrderStatusConsumer(ILogger<CheckOrderStatusConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderStatusRequest> context)
    {
        _logger.LogInformation("Received Order Status Request for Order Id: {OrderId}", context.Message.OrderId);

        await context.RespondAsync<OrderStatusResponse>(new
        {
            OrderId = context.Message.OrderId,
            Timestamp = DateTime.UtcNow,
            StatusCode = 200,
            StatusText = "Order Received - Awaiting Fulfillment"
        });
    }
}
