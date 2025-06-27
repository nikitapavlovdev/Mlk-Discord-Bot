using MlkAdmin._1_Domain.Interfaces;
using Microsoft.Extensions.Logging;
using MlkAdmin.Infrastructure.JsonModels.Users;
using Newtonsoft.Json;
using MlkAdmin.Infrastructure.JsonModels.Roles;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordUsersLobbyProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordUsersLobbyProvider> _logger;
        private readonly string _filePath;
        public RootDiscordUsersLobby? RootDiscordUsersLobby { get; set; }

        public JsonDiscordUsersLobbyProvider(string filePath, ILogger<JsonDiscordUsersLobbyProvider> logger)
        {
            _logger = logger;
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordUsersLobby = JsonConvert.DeserializeObject<RootDiscordUsersLobby>(File.ReadAllText(_filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
