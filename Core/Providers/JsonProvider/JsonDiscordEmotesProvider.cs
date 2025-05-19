using Newtonsoft.Json;
using MlkAdmin.Infrastructure.JsonModels.Emotes;
using Microsoft.Extensions.Logging;

namespace MlkAdmin.Core.Providers.JsonProvider
{
    public class JsonDiscordEmotesProvider
    {
        public RootDiscordEmotes? RootDiscordEmotes { get; set; }

        public JsonDiscordEmotesProvider(string filePath, ILogger<JsonDiscordEmotesProvider> logger)
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