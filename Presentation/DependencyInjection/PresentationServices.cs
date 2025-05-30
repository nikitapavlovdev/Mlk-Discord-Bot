
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using MlkAdmin.Presentation.Controllers.DiscordCommandsController;
using MlkAdmin.Presentation.Controllers.DiscordEventsController;
using Discord.Commands;

namespace MlkAdmin.Presentation.DependencyInjection
{
    public static class PresentationServices
    {

        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddSingleton<DiscordEventsController>();
            services.AddSingleton<DiscordCommandsController>();
            services.AddSingleton<CommandService>();

            DiscordSocketConfig discordConfiguration = new()
            {
                GatewayIntents = GatewayIntents.All
            };

            services.AddSingleton(new DiscordSocketClient(discordConfiguration));

            return services;
        }

    }
}
