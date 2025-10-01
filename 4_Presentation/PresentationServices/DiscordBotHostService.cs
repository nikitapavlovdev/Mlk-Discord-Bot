using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin.Presentation.DiscordListeners;
using Microsoft.Extensions.DependencyInjection;

namespace MlkAdmin.Presentation.PresentationServices
{
    public class DiscordBotHostService(
        ILogger <DiscordBotHostService> logger,
        IServiceProvider serviceProvider,
        DiscordSocketClient discordClient,
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

            using var scope = serviceProvider.CreateScope();

            DiscordEventsListener discordEventsController = scope.ServiceProvider.GetRequiredService<DiscordEventsListener>();
            discordEventsController.SubscribeOnEvents(discordClient);

            await discordClient.LoginAsync(TokenType.Bot, MlkAdminBotApiKey);
            await discordClient.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await discordClient.StopAsync();
        }
    }
}