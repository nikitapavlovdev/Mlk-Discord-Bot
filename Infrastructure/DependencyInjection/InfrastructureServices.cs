using Microsoft.Extensions.DependencyInjection;
using MlkAdmin.Infrastructure.Cache;

namespace MlkAdmin.Infrastructure.DependencyInjection
{
    public static class InfrastructureServices
    {

        public static IServiceCollection AddInfastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ChannelsCache>();
            services.AddSingleton<RolesCache>();
            services.AddSingleton<EmotesCache>();
            services.AddSingleton<AutorizationCache>();

            return services;
        }

    }
}
