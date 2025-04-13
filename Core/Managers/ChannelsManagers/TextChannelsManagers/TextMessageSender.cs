using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Discord;
using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Infrastructure.Cash;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers
{
    public class TextMessageSender(ILogger<TextMessageSender> logger, 
        ExtensionEmbedMessage extensionEmbedMessage,
        EmotesCash emotesCash,
        RolesCash rolesCash, 
        JsonChannelsMapProvider channelsProvider,
        JsonDiscordEmotesProvider emotesProvider, 
        JsonDiscordRolesProvider rolesProvider)
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
            await modal.FollowupAsync(embed: extensionEmbedMessage.GetSuccesAuthorizationMessageEmbedTemplate(
                emotesCash.GetEmote(emotesProvider.RootDiscordEmotes.AnimatedEmotes.AnimatedZero.Paceout.Id),
                rolesCash.GetRole(rolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Id),
                channelsProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Id,
                channelsProvider.RootChannel.Channels.TextChannels.ServerCategory.BotCommands.Id,
                channelsProvider.RootChannel.Channels.TextChannels.ServerCategory.News.Id), 
                ephemeral: true);
        }
        public async Task SendFollowupMessageOnErrorAutorization(SocketModal modal)
        {
            GuildEmote? emoteError = emotesCash.GetEmote(emotesProvider.RootDiscordEmotes.StaticEmotes.StaticZero.Hmph.Id);

            await modal.FollowupAsync(embed: extensionEmbedMessage.GetErrorAuthorizationMessageEmbedTemplate(emoteError), ephemeral: true);
        }
    }
}
