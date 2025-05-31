using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using MlkAdmin.Core.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin.Presentation.Controllers;

namespace MlkAdmin.Presentation.DependencyInjection.HostedServices
{
    public class PresentationHostedServices(
        ILogger <PresentationHostedServices> logger,
        DiscordSocketClient discordClient,
        DiscordEventsController discordEventsController,
        JsonDiscordConfigurationProvider discordBotConfig) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {

            string? MlkAdminBotApiKey = discordBotConfig
                .RootDiscordConfiguration?
                .MalenkieAdminBot?
                .API_KEY;

            if (string.IsNullOrWhiteSpace(MlkAdminBotApiKey))
            {
                logger.LogWarning("API key not found");
                return;
            }

            discordEventsController.SubscribeOnEvents(discordClient);

            await discordClient.LoginAsync(TokenType.Bot, MlkAdminBotApiKey);
            await discordClient.StartAsync();

            logger.LogInformation("Bot successfully logged in and started.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await discordClient.StopAsync();

            logger.LogInformation("Bot is shutting down...");
        }
    }
}