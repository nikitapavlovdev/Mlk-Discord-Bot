using Discord.WebSocket;
using Discord;
using Discord_Bot.Core.Utilities.General;
using Discord_Bot.Infrastructure.Cache;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.RolesManagers;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;

namespace Discord_Bot.Core.Managers.UserManagers;

public class AutorizationManager(ILogger<AutorizationManager> logger, 
    AutorizationCache auCache, 
    RolesManager rolesManagers,
    TextMessageManager channelMessageManagers)
{
    public async Task SendAutorizationCode(SocketGuildUser socketGuildUser)
    {
        try
        {
            string auCode = GetAutorizationCode();
            auCache.SetTemporaryCodes(socketGuildUser, auCode);

            await socketGuildUser.SendMessageAsync($"Твой код авторизации: `{auCode}`");
        }
        catch (Exception ex)
        {
            logger.LogError("Error: {Messsage} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);

        }
    }
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
    private static string GetAutorizationCode()
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
