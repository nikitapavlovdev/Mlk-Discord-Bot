using MlkAdmin._1_Domain.Interfaces.Discord;
using Newtonsoft.Json;
using MlkAdmin._3_Infrastructure.JsonModels.Emotes;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordEmotesProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordEmotesProvider> logger;
        private readonly string filePath;
        private RootDiscordEmotes? RootDiscordEmotes { get; set; } 

        public JsonDiscordEmotesProvider(string filePath, ILogger<JsonDiscordEmotesProvider> logger)
        {
            this.logger = logger;
            this.filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordEmotes = JsonConvert.DeserializeObject<RootDiscordEmotes>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}