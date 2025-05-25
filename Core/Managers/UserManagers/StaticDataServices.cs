using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Providers.JsonProvider;

namespace MlkAdmin.Core.Managers.UserManagers
{
    public class StaticDataServices(
        ILogger<StaticDataServices> logger,
        JsonDiscordUsersLobbyProvider jsonDiscordUsersLobbyProvider)
    {
        private readonly Dictionary<ulong, string> UniqueLobbyNames = [];
        public async Task LoadStaticData()
        {
            await LoadUsersLobbyNames();
        }

        private async Task LoadUsersLobbyNames()
        {
            AddUniqueLobbyNameFromJson(
                jsonDiscordUsersLobbyProvider.RootDiscordUsersLobby.User.GuzMan.Id,
                jsonDiscordUsersLobbyProvider.RootDiscordUsersLobby.User.GuzMan.LobbyName);

            AddUniqueLobbyNameFromJson(
                jsonDiscordUsersLobbyProvider.RootDiscordUsersLobby.User.Ronin.Id,
                jsonDiscordUsersLobbyProvider.RootDiscordUsersLobby.User.Ronin.LobbyName);


            await Task.CompletedTask;
        }

        public string GetUniqueLobbyName(ulong userId)
        {
            try
            {
                return UniqueLobbyNames.TryGetValue(userId, out string? name) ? name : string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                return "";
            }
        }

        private void AddUniqueLobbyNameFromJson(ulong key, string name)
        {
            UniqueLobbyNames.TryAdd(key, name);
        }
    }
}
