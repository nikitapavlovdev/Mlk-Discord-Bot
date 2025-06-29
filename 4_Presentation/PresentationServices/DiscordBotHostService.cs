using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin.Presentation.DiscordListeners;

namespace MlkAdmin.Presentation.PresentationServices
{
    public class DiscordBotHostService(
        ILogger <DiscordBotHostService> logger,
        DiscordSocketClient discordClient,
        DiscordEventsListener discordEventsController,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            string? MlkAdminBotApiKey = jsonDiscordConfigurationProvider.ApiKey;
               

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