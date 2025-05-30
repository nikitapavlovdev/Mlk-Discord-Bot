using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Discord;
using Discord.WebSocket;
using MlkAdmin.Core.Providers.JsonProvider;
using MlkAdmin.Presentation.Controllers.DiscordEventsController;
using MlkAdmin.Presentation.Controllers.DiscordCommandsController;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.DependencyInjection;
using MlkAdmin.Infrastructure.DependencyInjection;
using MlkAdmin.Presentation.DependencyInjection;

namespace MlkAdmin.Presentation.DiscordAPI
{
    class Startup
    {
        public static async Task Main()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddCoreServices();
                    services.AddInfastructureServices();
                    services.AddPresentationServices();
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .Build();

            DiscordSocketClient botClient = host.Services.GetRequiredService<DiscordSocketClient>();
            DiscordEventsController eventsController = host.Services.GetRequiredService<DiscordEventsController>();
            DiscordCommandsController commandsController = host.Services.GetRequiredService<DiscordCommandsController>();
            JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider = host.Services.GetRequiredService<JsonDiscordConfigurationProvider>();

            string? discordToken = jsonDiscordConfigurationProvider.RootDiscordConfiguration?.MalenkieAdminBot?.API_KEY;

            if (discordToken != null)
            {
                await botClient.LoginAsync(TokenType.Bot, discordToken);
            }

            await botClient.StartAsync();
            await Task.Delay(-1);
        }
    }
}