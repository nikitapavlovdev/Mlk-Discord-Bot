using Discord.WebSocket;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.RolesManagers;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;

namespace MlkAdmin._2_Application.Managers.UserManagers;

public class AutorizationManager( 
    RolesManager rolesManagers,
    JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider,
    ILogger<AutorizationManager> logger,
    DiscordSocketClient client,
    IModeratorLogsSender moderatorLogsSender,
    JsonDiscordChannelsMapProvider jsonChannelsMapProvider)
{
    public async Task AuthorizeUser(ulong socketGuildUserId)
    {
        try
        {
            SocketGuild socketGuild = client.GetGuild(jsonDiscordConfigurationProvider.GuildId);
            SocketGuildUser socketGuildUser = socketGuild.GetUser(socketGuildUserId);

            if(socketGuildUser == null)
            {
                return;
            }

            await Task.WhenAll(
                rolesManagers.DeleteNotRegisteredRoleAsync(socketGuildUser),
                rolesManagers.AddBaseServerRoleAsync(socketGuildUser),
                rolesManagers.AddGamerRoleAsync(socketGuildUser),
                moderatorLogsSender.SendLogMessageAsync(new DTOs.LogMessageDto()
                {
                    ChannelId = jsonChannelsMapProvider.LogsChannelId,
                    Description = $"> Пользователь {socketGuildUser.Mention} завершил верификацию.",
                    GuildId = socketGuild.Id,
                    UserId = socketGuildUserId,
                    Title = "Успешная верификация"
                }));
            
        }
        catch (Exception ex)
        {
            logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        }
    }
}
