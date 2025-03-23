using Discord.WebSocket;
using Discord_Bot.Core.Utilities.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Discord;
using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Infrastructure.Cash;

namespace Discord_Bot.Core.Managers.ChannelMessageManagers
{
    public class ChannelMessageManager(ILogger<ChannelMessageManager> _logger, 
        IConfiguration _configuration, 
        ExtensionEmbedMessage _extensionEmbedMessage,
        EmotesCash _emotesCash,
        RolesCash _rolesCash)
    {
        public async Task SendWelcomeMessageAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                SocketTextChannel textChannel = socketGuildUser.Guild.TextChannels.FirstOrDefault(x => x.Id == ExtensionMethods.ConvertId(_configuration["WelcomeSettings:ChannelId"]));
                MessageComponent auMessageComponent = ExtensionMessageComponents.GetWelcomeMessageComponent(socketGuildUser.Id);
                Embed embedMessage = _extensionEmbedMessage.GetJoinedEmbedTemplate(socketGuildUser);

                await textChannel.SendMessageAsync($"{socketGuildUser.Mention}", embed: embedMessage, components: auMessageComponent);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public async Task SendFollowupMessageOnSuccessAutorization(SocketModal modal)
        {
            ulong roleChannelId = ExtensionMethods.ConvertId(_configuration["RolesSettings:ChannelId"]);
            ulong botCommandChannelId = ExtensionMethods.ConvertId(_configuration["CommandsSettings:ChannelId"]);
            ulong newsChannelId = ExtensionMethods.ConvertId(_configuration["NewsSettings:ChannelId"]);

            SocketRole baseServerRole = _rolesCash.GetRole(ExtensionMethods.ConvertId(_configuration["Roles:MalenkiyMember:Id"]));
            GuildEmote emoteSuccess = _emotesCash.GetEmote(ExtensionMethods.ConvertId(_configuration["animated:zero_peaceout:id"]));

            await modal.FollowupAsync(embed: _extensionEmbedMessage.GetSuccesAuthorizationMessageEmbedTemplate(emoteSuccess, baseServerRole, roleChannelId, botCommandChannelId, newsChannelId), ephemeral: true);
        }
        public async Task SendFollowupMessageOnErrorAutorization(SocketModal modal)
        {
            GuildEmote emoteError = _emotesCash.GetEmote(ExtensionMethods.ConvertId(_configuration["static:zero_hmph:id"]));

            await modal.FollowupAsync(embed: _extensionEmbedMessage.GetErrorAuthorizationMessageEmbedTemplate(emoteError), ephemeral: true);
        }
        public async Task SendFollowupMessageOnSuccesInputPersonalData(SocketModal modal)
        {
            await Task.CompletedTask;
        }
    }
}
