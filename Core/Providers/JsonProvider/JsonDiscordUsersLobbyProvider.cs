using Microsoft.Extensions.Logging;
using MlkAdmin.Infrastructure.JsonModels.Users;
using Newtonsoft.Json;

namespace MlkAdmin.Core.Providers.JsonProvider
{
    public class JsonDiscordUsersLobbyProvider
    {
        public RootDiscordUsersLobby? RootDiscordUsersLobby { get; set; }

        public JsonDiscordUsersLobbyProvider(string filePath, ILogger<JsonDiscordUsersLobbyProvider> logger)
        {
            try
            {
                RootDiscordUsersLobby = JsonConvert.DeserializeObject<RootDiscordUsersLobby>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
