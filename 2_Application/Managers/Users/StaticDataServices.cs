using Microsoft.Extensions.Logging;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Managers.UserManagers
{
    public class StaticDataServices(
        ILogger<StaticDataServices> logger,
        JsonDiscordUsersLobbyProvider jsonDiscordUsersLobbyProvider)
    {
        public string GetUniqueLobbyName(ulong userId)
        {
            try
            {
                return jsonDiscordUsersLobbyProvider.UsersLobbyNames.TryGetValue(userId, out string? name) ? name : string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                return "";
            }
        }

    }
}
