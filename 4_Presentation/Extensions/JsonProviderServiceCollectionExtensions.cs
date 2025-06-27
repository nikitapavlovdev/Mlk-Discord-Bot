using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces;

namespace MlkAdmin._4_Presentation.Extensions
{
    public static class JsonProviderServiceCollectionExtensions
    {
        public static IServiceCollection AddJsonProvider<T>(this IServiceCollection services, string filePath) where T : class, IJsonConfigurationProvider
        {
            services.AddSingleton<T>(x =>
            {
                string fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath));
                ILogger<T> logger = x.GetRequiredService<ILogger<T>>();

                return (T)Activator.CreateInstance(typeof(T), fullPath, logger)!;
            });

            return services;
        }
    }
}
