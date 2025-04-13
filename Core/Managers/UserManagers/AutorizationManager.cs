using Discord.WebSocket;
using Discord;
using Discord_Bot.Core.Utilities.General;
using Discord_Bot.Infrastructure.Cash;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.RolesManagers;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;

namespace Discord_Bot.Core.Managers.UserManagers;

public class AutorizationManager(ILogger<AutorizationManager> logger, 
    AuCash auCash, 
    RolesManager rolesManagers,
    TextMessageSender channelMessageManagers)
{
    public async Task SendAutorizationCode(SocketGuildUser socketGuildUser)
    {
        try
        {
            string auCode = GetAutorizationCode();
            auCash.SetTemporaryCodes(socketGuildUser, auCode);

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
            
            auCash.RemoveCodeFromDict(socketGuildUser);
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
        string fromDictrCode = auCash.GetCodeForUser(socketGuildUser, out string? def);

        return fromModalCode == fromDictrCode;
    }
}
