using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using MlkAdmin.Presentation.Controllers;
using MlkAdmin.Presentation.DependencyInjection.HostedServices;

namespace MlkAdmin.Presentation.DependencyInjection.Registrations
{
    public static class PresentetionServices
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddHostedService<PresentationHostedServices>();
            services.AddSingleton<DiscordEventsController>();
            services.AddSingleton<CommandService>();
            services.AddSingleton(new DiscordSocketClient(new()
            {
                GatewayIntents = GatewayIntents.All
            }
            ));

            return services;
        }
    }
}
