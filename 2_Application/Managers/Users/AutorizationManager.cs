using Discord;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._2_Application.DTOs.Messages;
using MlkAdmin._1_Domain.Interfaces.Roles;
using Discord.WebSocket;

namespace MlkAdmin._2_Application.Managers.UserManagers
{
    public class AutorizationManager( 
        ILogger<AutorizationManager> logger,
        IRoleCenter roleCenter,
        IModeratorLogsSender moderatorLogsSender,
        JsonDiscordRolesProvider jsonDiscordRolesProvider,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider)
    {
        public async Task AuthorizeUser(IUser user)
        {
            try
            {
                if (user is not SocketGuildUser guildUser)
                {
                    return;
                }

                await Task.WhenAll(

                    roleCenter.RemoveRoleFromUserAsync(guildUser.Id,
                    jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.NotRegistered.Id),

                    roleCenter.AddRolesToUserAsync(guildUser.Id,
                    [
                        jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Id,
                        jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Gamer.Id
                    ]),

                    moderatorLogsSender.SendLogMessageAsync(new LogMessageDto()
                    {
                        ChannelId = jsonChannelsMapProvider.LogsChannelId,
                        Description = $"> Пользователь {guildUser.Mention} завершил верификацию.",
                        UserId = guildUser.Id,
                        Title = "Успешная верификация"
                    })
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при авторизации пользователя");
            }
        }
    }
}