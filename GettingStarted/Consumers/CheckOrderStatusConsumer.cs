using GettingStarted.Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace GettingStarted.Consumers;

public class CheckOrderStatusConsumer : IConsumer<OrderStatusRequest>
{
    public async Task Consume(ConsumeContext<OrderStatusRequest> context)
    {
        await context.RespondAsync<OrderStatusResponse>(new
        {
            OrderId = context.Message.OrderId,
            Timestamp = DateTime.UtcNow,
            StatusCode = 200,
            StatusText = "Order Received - Awaiting Fulfillment"
        });
    }
}
