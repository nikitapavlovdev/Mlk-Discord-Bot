using Discord.WebSocket;
using Discord_Bot.Core.Utilities.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Discord;
using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Infrastructure.Cash;
using Discord_Bot.Core.Providers.JsonProvider;
using Discord_Bot.Infrastructure.JsonModels.Channels;

namespace Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers
{
    public class TextMessageSender(ILogger<TextMessageSender> _logger, 
        IConfiguration _configuration, 
        ExtensionEmbedMessage _extensionEmbedMessage,
        EmotesCash _emotesCash,
        RolesCash _rolesCash, 
        JsonVoiceChannelsProvider _provider)
    {
        public async Task SendWelcomeMessageAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                RootVoiceChannels? root = _provider.GetRootChannels();
                SocketTextChannel? textChannel = socketGuildUser.Guild.TextChannels.FirstOrDefault(x => x.Id == root?.Channels?.TextChannels?.ServerCategory?.Starting?.Id);
                MessageComponent auMessageComponent = ExtensionMessageComponents.GetWelcomeMessageComponent(socketGuildUser.Id);
                Embed embedMessage = _extensionEmbedMessage.GetJoinedEmbedTemplate(socketGuildUser);

                if(textChannel is null)
                {
                    return;
                }

                await textChannel.SendMessageAsync($"{socketGuildUser.Mention}", embed: embedMessage, components: auMessageComponent);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public async Task SendFollowupMessageOnSuccessAutorization(SocketModal modal)
        {
            ulong roleChannelId = _configuration["RolesSettings:ChannelId"].ConvertId();
            ulong botCommandChannelId = _configuration["CommandsSettings:ChannelId"].ConvertId();
            ulong newsChannelId = _configuration["NewsSettings:ChannelId"].ConvertId();

            SocketRole baseServerRole = _rolesCash.GetRole(_configuration["Roles:MalenkiyMember:Id"].ConvertId());
            GuildEmote? emoteSuccess = _emotesCash.GetEmote(_configuration["animated:zero_peaceout:id"].ConvertId());

            await modal.FollowupAsync(embed: _extensionEmbedMessage.GetSuccesAuthorizationMessageEmbedTemplate(emoteSuccess, baseServerRole, roleChannelId, botCommandChannelId, newsChannelId), ephemeral: true);
        }
        public async Task SendFollowupMessageOnErrorAutorization(SocketModal modal)
        {
            GuildEmote? emoteError = _emotesCash.GetEmote(_configuration["static:zero_hmph:id"].ConvertId());

            await modal.FollowupAsync(embed: _extensionEmbedMessage.GetErrorAuthorizationMessageEmbedTemplate(emoteError), ephemeral: true);
        }
        public static async Task SendFollowupMessageOnSuccesInputPersonalData(SocketModal modal)
        {
            await modal.FollowupAsync("Any text", ephemeral: true);
        }
    }
}
