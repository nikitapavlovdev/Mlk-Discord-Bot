using Microsoft.Extensions.Logging;
using Discord_Bot.Infrastructure.JsonModels.Configuration;
using Newtonsoft.Json;

namespace Discord_Bot.Core.Providers.JsonProvider
{
    public class JsonDiscordConfigurationProvider
    {
        public RootDiscordConfiguration? RootDiscordConfiguration { get; set; }

        public JsonDiscordConfigurationProvider(string filePath, ILogger<JsonDiscordConfigurationProvider> logger)
        {
            try
            {
                RootDiscordConfiguration = JsonConvert.DeserializeObject<RootDiscordConfiguration>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}