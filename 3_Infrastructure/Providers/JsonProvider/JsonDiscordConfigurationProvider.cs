using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin.Infrastructure.JsonModels.Configuration;
using Newtonsoft.Json;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordConfigurationProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordConfigurationProvider> _logger;
        private readonly string _filePath;
        public RootDiscordConfiguration? RootDiscordConfiguration { get; set; }

        public JsonDiscordConfigurationProvider(string filePath, ILogger<JsonDiscordConfigurationProvider> logger)
        {
            _logger = logger;
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordConfiguration = JsonConvert.DeserializeObject<RootDiscordConfiguration>(File.ReadAllText(_filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}