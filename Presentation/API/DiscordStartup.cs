using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.DependencyInjection;
using MlkAdmin.Infrastructure.DependencyInjection;
using MlkAdmin.Presentation.DependencyInjection.GeneralServices;

namespace MlkAdmin.Presentation.API
{
    class DiscordStartup
    {
        public static async Task Main()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services.AddCoreServices();
                    services.AddInfastructureServices();
                    services.AddPresentationServices();
                })
                .ConfigureLogging((logging) =>
                {
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .Build();

            await host.RunAsync();
        }
    }
}