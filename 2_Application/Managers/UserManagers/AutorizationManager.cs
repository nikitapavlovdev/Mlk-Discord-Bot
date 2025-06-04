using Discord.WebSocket;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin.Application.Managers.RolesManagers;

namespace MlkAdmin.Application.Managers.UserManagers;

public class AutorizationManager( 
    RolesManager rolesManagers,
    JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider,
    ILogger<AutorizationManager> logger,
    DiscordSocketClient client)
{
    public async Task AuthorizeUser(ulong socketGuildUserId)
    {
        try
        {
            SocketGuild socketGuild = client.GetGuild(jsonDiscordConfigurationProvider.RootDiscordConfiguration.Guild.Id);
            SocketGuildUser socketGuildUser = socketGuild.GetUser(socketGuildUserId);

            if(socketGuildUser == null)
            {
                return;
            }

            await Task.WhenAll(
                rolesManagers.DeleteNotRegisteredRoleAsync(socketGuildUser),
                rolesManagers.AddBaseServerRoleAsync(socketGuildUser),
                rolesManagers.AddGamerRoleAsync(socketGuildUser));
        }
        catch (Exception ex)
        {
            logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        }
    }
}
