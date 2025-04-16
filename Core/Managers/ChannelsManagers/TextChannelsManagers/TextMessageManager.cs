using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Discord;
using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Infrastructure.Cache;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers
{
    public class TextMessageManager(ILogger<TextMessageManager> logger, 
        ExtensionEmbedMessage extensionEmbedMessage,
        EmotesCache emotesCache,
        RolesCache rolesCache, 
        JsonChannelsMapProvider channelsProvider,
        JsonDiscordEmotesProvider emotesProvider, 
        JsonDiscordRolesProvider rolesProvider,
        JsonChannelsMapProvider jsonChannelsMapProvider,
        ExtensionSelectionMenu extensionSelectionMenu,
        ChannelsCache channelsCache)
    {
        
        private async Task LoadTextChannelsFromGuild(SocketGuild socketGuild)
        {
            foreach (SocketTextChannel channel in socketGuild.TextChannels)
            {
                channelsCache.AddTextChannel(channel);
            }

            await Task.CompletedTask;
        }
        public async Task GuildTextChannelsInitialization(SocketGuild socketGuild)
        {
            try
            {
                await LoadTextChannelsFromGuild(socketGuild);
                logger.LogInformation("Guild Text Channels has been loaded");
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}", ex.Message);
            }
        }
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
                emotesCache.GetEmote(emotesProvider.RootDiscordEmotes.AnimatedEmotes.AnimatedZero.Paceout.Id),
                rolesCache.GetRole(rolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Id),
                channelsProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Id,
                channelsProvider.RootChannel.Channels.TextChannels.ServerCategory.BotCommands.Id,
                channelsProvider.RootChannel.Channels.TextChannels.ServerCategory.News.Id), 
                ephemeral: true);
        }
        public async Task SendFollowupMessageOnErrorAutorization(SocketModal modal)
        {
            GuildEmote? emoteError = emotesCache.GetEmote(emotesProvider.RootDiscordEmotes.StaticEmotes.StaticZero.Hmph.Id);

            await modal.FollowupAsync(embed: extensionEmbedMessage.GetErrorAuthorizationMessageEmbedTemplate(emoteError), ephemeral: true);
        }
        public async Task SendMessageWithGuildRoles(SocketGuild socketGuild)
        {
            SocketTextChannel? textChannel = socketGuild.TextChannels.FirstOrDefault(x => x.Id == (jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Id));
            MessageComponent component = extensionSelectionMenu.GetRolesSelectionMenu();

            if (textChannel == null)
            {
                return;
            }

            await ExtensionChannelsManager.DeleteAllMessageFromChannel(textChannel);
            await extensionEmbedMessage.SendRolesMessage(textChannel, component);
        }
    }
}
