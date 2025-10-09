using MlkAdmin._1_Domain.Interfaces.Discord;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordUsersLobbyProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordUsersLobbyProvider> logger;
        private readonly string filePath;
        public Dictionary<ulong, string>? UsersLobbyNames { get; set; }

        public JsonDiscordUsersLobbyProvider(string filePath, ILogger<JsonDiscordUsersLobbyProvider> logger)
        {
            this.logger = logger;
            this.filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                UsersLobbyNames = JsonConvert.DeserializeObject<Dictionary<ulong, string>>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
