using MlkAdmin._1_Domain.Interfaces;
using Newtonsoft.Json;
using MlkAdmin.Infrastructure.JsonModels.Emotes;
using Microsoft.Extensions.Logging;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordEmotesProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordEmotesProvider> _logger;
        private readonly string _filePath;
        public RootDiscordEmotes? RootDiscordEmotes { get; set; } 

        public JsonDiscordEmotesProvider(string filePath, ILogger<JsonDiscordEmotesProvider> logger)
        {
            _logger = logger;
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordEmotes = JsonConvert.DeserializeObject<RootDiscordEmotes>(File.ReadAllText(_filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}