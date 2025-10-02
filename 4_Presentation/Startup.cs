using Serilog;
using Microsoft.Extensions.Hosting;
using MlkAdmin.Presentation.DI;

namespace MlkAdmin.Presentation
{
    class Startup
    {
        public static async Task Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            IHost host = Host.CreateDefaultBuilder()
                .UseSerilog()
                .ConfigureServices((services) =>
                {
                    services.AddDomainServices();
                    services.AddApplicationServices();
                    services.AddInfrastructureServices();
                    services.AddPresentationServices();
                })
                .Build();

            await host.RunAsync();
        }
    }
}