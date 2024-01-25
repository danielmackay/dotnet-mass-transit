using GettingStarted.Consumers;
using GettingStarted.Contracts;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace GettingStarted
{
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

                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumers(entryAssembly);

                        // TODO: Do we need to add consumers explicitly?
                        x.AddConsumer<CheckOrderStatusConsumer>()
                            .Endpoint(e => e.Name = "order-status");

                        // Sends the request to the specified address, instead of publishing it
                        x.AddRequestClient<OrderStatusRequest>(new Uri("exchange:order-status"));

                        // NOTE: Sagas are not used for simple messaging
                        // x.AddSagaStateMachines(entryAssembly);
                        // x.AddSagas(entryAssembly);
                        // x.AddActivities(entryAssembly);
                        // By default, sagas are in-memory, but should be changed to a durable
                        // saga repository.
                        //x.SetInMemorySagaRepositoryProvider();

                        x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
                    });

                    //services.AddHostedService<Worker>();
                    services.AddHostedService<RequestWorker>();
                });
    }
}
