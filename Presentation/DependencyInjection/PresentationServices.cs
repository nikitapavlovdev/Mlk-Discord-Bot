using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using MlkAdmin.Presentation.HostedServices;
using MlkAdmin.Presentation.Controllers;

namespace MlkAdmin.Presentation.DependencyInjection
{
    public static class PresentationServices
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            DiscordSocketConfig discordConfiguration = new()
            {
                GatewayIntents = GatewayIntents.All
            };

            services.AddHostedService<MlkAdminHostedServices>();

            services.AddSingleton<DiscordEventsController>();
            services.AddSingleton<CommandService>();
            services.AddSingleton(new DiscordSocketClient(discordConfiguration));

            return services;
        }

    }
}
