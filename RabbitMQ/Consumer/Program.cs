using Consumer.Consumers;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading.Tasks;

namespace Consumer;

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

                    x.UsingRabbitMq((context,cfg) =>
                    {
                        cfg.Host("localhost", "/", h => {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                });
            });
}
