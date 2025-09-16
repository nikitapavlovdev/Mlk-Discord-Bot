using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin.Presentation.DiscordListeners;
using Microsoft.Extensions.DependencyInjection;
using MlkAdmin._4_Presentation.Discord;

namespace MlkAdmin.Presentation.PresentationServices
{
    public class DiscordBotHostService(
        ILogger <DiscordBotHostService> logger,
        DiscordSocketClient discordClient,
        IServiceProvider serviceProvider,
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

            Console.WriteLine("Подключены команды");

            await discordClient.LoginAsync(TokenType.Bot, MlkAdminBotApiKey);
            await discordClient.StartAsync();

            logger.LogInformation("Bot successfully logged in and started.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await discordClient.StopAsync();

            logger.LogInformation("Bot is shutting down.");
        }
    }
}