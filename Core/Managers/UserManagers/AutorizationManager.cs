using Discord.WebSocket;
using MlkAdmin.Core.Utilities.General;
using MlkAdmin.Infrastructure.Cache;
using MlkAdmin.Core.Managers.RolesManagers;
using MlkAdmin.Core.Managers.ChannelsManagers.TextChannelsManagers;
using MlkAdmin.Core.Providers.JsonProvider;

namespace MlkAdmin.Core.Managers.UserManagers;

public class AutorizationManager( 
    AutorizationCache auCache, 
    RolesManager rolesManagers,
    TextMessageManager channelMessageManagers,
    JsonDiscordRolesProvider jsonDiscordRolesProvider)
{
    public async Task AuthorizeUser(SocketModal modal, SocketGuildUser socketGuildUser)
    {
        if (IsValidCode(modal, socketGuildUser))
        {
            if(!socketGuildUser.Roles.Any(x => x.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Id))
            {
                await Task.WhenAll(
                rolesManagers.DeleteNotRegisteredRoleAsync(socketGuildUser),
                rolesManagers.AddBaseServerRoleAsync(socketGuildUser),
                rolesManagers.AddGamerRoleAsync(socketGuildUser),
                channelMessageManagers.SendFollowupMessageOnSuccessAutorization(modal));
            }
            else
            {
                await channelMessageManagers.SendFollowupMessageOnSuccessAutorization(modal);
            }

            auCache.RemoveCodeFromDict(socketGuildUser);
        }
        else
        {
            await channelMessageManagers.SendFollowupMessageOnErrorAutorization(modal);
        }
    }
    public static string GetAutorizationCode()
    {
        return ExtensionStaticMethods.GetRandomCode(10);
    }
    private bool IsValidCode(SocketModal modal, SocketGuildUser socketGuildUser)
    {
        string fromModalCode = modal.Data.Components.First(x => x.CustomId == "au_selection_input").Value;
        string fromDictrCode = auCache.GetCodeForUser(socketGuildUser, out string? def);

        return fromModalCode == fromDictrCode;
    }
}
