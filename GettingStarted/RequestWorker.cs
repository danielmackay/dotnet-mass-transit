using GettingStarted.Contracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GettingStarted;

public class RequestWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RequestWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // NOTE: Hack to fix DI issue
            using var scope = _serviceProvider.CreateScope();
            var client = scope.ServiceProvider.GetRequiredService<IRequestClient<OrderStatusRequest>>();

            Console.WriteLine("Sending request...");

            var response = await client.GetResponse<OrderStatusResponse>(new OrderStatusRequest() { OrderId = "123" }, stoppingToken);

            Console.WriteLine($"Order Status: {response.Message.StatusText}");

            await Task.Delay(1000, stoppingToken);
        }
    }
}
