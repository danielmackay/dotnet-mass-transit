using Contracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Producer.Workers;
using System;
using System.Threading.Tasks;

namespace Producer;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMassTransit(x =>
                {
                    x.SetKebabCaseEndpointNameFormatter();

                    // Sends the request to the specified address, instead of publishing it
                    x.AddRequestClient<OrderStatusRequest>(new Uri("exchange:order-status"));

                    x.UsingRabbitMq((context,cfg) =>
                    {
                        cfg.Host("localhost", "/", h => {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                });

                //services.AddHostedService<ProducerWorker>();
                services.AddHostedService<RequestProducerWorker>();
            });
}
