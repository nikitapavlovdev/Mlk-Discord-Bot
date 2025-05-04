using Discord.WebSocket;
using Discord;
using Discord_Bot.Core.Utilities.General;
using Discord_Bot.Infrastructure.Cache;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.RolesManagers;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;

namespace Discord_Bot.Core.Managers.UserManagers;

public class AutorizationManager( 
    AutorizationCache auCache, 
    RolesManager rolesManagers,
    TextMessageManager channelMessageManagers)
{
    public async Task AuthorizeUser(SocketModal modal, SocketGuildUser socketGuildUser)
    {
        if (IsValidCode(modal, socketGuildUser))
        {
            await Task.WhenAll(
                rolesManagers.DeleteNotRegisteredRoleAsync(socketGuildUser),
                rolesManagers.AddBaseServerRoleAsync(socketGuildUser),
                channelMessageManagers.SendFollowupMessageOnSuccessAutorization(modal)
            );

            auCache.RemoveCodeFromDict(socketGuildUser);
        }
        else
        {
            await channelMessageManagers.SendFollowupMessageOnErrorAutorization(modal);
        }
    }
    public static string GetAutorizationCode()
    {
        return ExtensionMethods.GetRandomCode(10);
    }
    private bool IsValidCode(SocketModal modal, SocketGuildUser socketGuildUser)
    {
        string fromModalCode = modal.Data.Components.First(x => x.CustomId == "au_selection_input").Value;
        string fromDictrCode = auCache.GetCodeForUser(socketGuildUser, out string? def);

        return fromModalCode == fromDictrCode;
    }
}
