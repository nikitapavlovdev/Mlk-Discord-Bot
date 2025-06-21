using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MlkAdmin.Presentation.DI;

namespace MlkAdmin.Presentation
{
    class Startup
    {
        public static async Task Main()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services.AddDomainServices();
                    services.AddApplicationServices();
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