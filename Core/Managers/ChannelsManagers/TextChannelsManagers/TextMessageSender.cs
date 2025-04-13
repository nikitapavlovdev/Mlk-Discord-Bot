using Discord.WebSocket;
using Discord_Bot.Core.Utilities.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Discord;
using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Infrastructure.Cash;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers
{
    public class TextMessageSender(ILogger<TextMessageSender> logger, 
        IConfiguration configuration, 
        ExtensionEmbedMessage extensionEmbedMessage,
        EmotesCash emotesCash,
        RolesCash rolesCash, 
        JsonChannelsMapProvider channelsProvider)
    {
        public async Task SendWelcomeMessageAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                SocketTextChannel? textChannel = socketGuildUser.Guild.TextChannels.FirstOrDefault(x => x.Id == channelsProvider.RootChannel?.Channels?.TextChannels?.ServerCategory?.Starting?.Id);
                MessageComponent auMessageComponent = ExtensionMessageComponents.GetWelcomeMessageComponent(socketGuildUser.Id);
                Embed embedMessage = extensionEmbedMessage.GetJoinedEmbedTemplate(socketGuildUser);

                if(textChannel is null)
                {
                    return;
                }

                await textChannel.SendMessageAsync($"{socketGuildUser.Mention}", embed: embedMessage, components: auMessageComponent);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public async Task SendFollowupMessageOnSuccessAutorization(SocketModal modal)
        {
            ulong roleChannelId = configuration["RolesSettings:ChannelId"].ConvertId(); 
            ulong botCommandChannelId = configuration["CommandsSettings:ChannelId"].ConvertId();
            ulong newsChannelId = configuration["NewsSettings:ChannelId"].ConvertId();

            SocketRole baseServerRole = rolesCash.GetRole(configuration["Roles:MalenkiyMember:Id"].ConvertId());
            GuildEmote? emoteSuccess = emotesCash.GetEmote(configuration["animated:zero_peaceout:id"].ConvertId());

            await modal.FollowupAsync(embed: extensionEmbedMessage.GetSuccesAuthorizationMessageEmbedTemplate(emoteSuccess, baseServerRole, roleChannelId, botCommandChannelId, newsChannelId), ephemeral: true);
        }
        public async Task SendFollowupMessageOnErrorAutorization(SocketModal modal)
        {
            GuildEmote? emoteError = emotesCash.GetEmote(configuration["static:zero_hmph:id"].ConvertId());

            await modal.FollowupAsync(embed: extensionEmbedMessage.GetErrorAuthorizationMessageEmbedTemplate(emoteError), ephemeral: true);
        }
        public static async Task SendFollowupMessageOnSuccesInputPersonalData(SocketModal modal)
        {
            await modal.FollowupAsync("Any text", ephemeral: true);
        }
    }
}
