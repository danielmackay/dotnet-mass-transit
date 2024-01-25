using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Producer.Workers;

public class ProducerWorker : BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<ProducerWorker> _logger;

    public ProducerWorker(IBus bus, ILogger<ProducerWorker> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Sending message...");

            await _bus.Publish(new Contracts.HelloWorld() { Value = $"The time is {DateTimeOffset.Now}" },
                stoppingToken);

            await Task.Delay(1000, stoppingToken);
        }
    }
}
