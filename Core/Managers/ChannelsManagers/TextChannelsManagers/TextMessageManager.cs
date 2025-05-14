using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Discord;
using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Infrastructure.Cache;
using Discord_Bot.Core.Providers.JsonProvider;
using Discord_Bot.Core.Managers.UserManagers;

namespace Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers
{
    public class TextMessageManager(ILogger<TextMessageManager> logger, 
        ExtensionEmbedMessage extensionEmbedMessage,
        EmotesCache emotesCache,
        JsonChannelsMapProvider channelsProvider,
        JsonDiscordEmotesProvider emotesProvider, 
        JsonChannelsMapProvider jsonChannelsMapProvider,
        JsonDiscordDynamicMessagesProvider jsonDiscordDynamicMessagesProvider,
        ChannelsCache channelsCache,
        ExtensionSelectionMenu extensionSelectionMenu,
        AutorizationCache autorizationCache)
    {
        #region Conrollers
        public async Task GuildTextChannelsInitialization(SocketGuild socketGuild)
        {
            try
            {
                await LoadTextChannelsFromGuild(socketGuild);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}", ex.Message);
            }
        }
        public async Task SendMessageWithGuildRoles(SocketGuild socketGuild)
        {
            await Task.WhenAll(
                SendMainGuildRolesMessage(socketGuild),
                SendSwitchColorRolesMessage(socketGuild),
                SendRulesMessage(socketGuild));
        }
        #endregion

        #region Private
        private async Task LoadTextChannelsFromGuild(SocketGuild socketGuild)
        {
            foreach (SocketTextChannel channel in socketGuild.TextChannels)
            {
                channelsCache.AddTextChannel(channel);
            }

            await Task.CompletedTask;
        }
        private async Task SendMainGuildRolesMessage(SocketGuild socketGuild)
        {
            SocketTextChannel? textRolesChannel = socketGuild.TextChannels.FirstOrDefault(x => x.Id == jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Id);

            if (await textRolesChannel.GetMessageAsync(jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.Roles.MainRoles.Id) is IUserMessage sentMessage)
            {
                await sentMessage.ModifyAsync(message =>
                {
                    message.Embed = extensionEmbedMessage.GetMainRolesEmbedMessage();
                });
            }
            else
            {
                await textRolesChannel.SendMessageAsync(embed: extensionEmbedMessage.GetMainRolesEmbedMessage());
            }
        }
        private async Task SendSwitchColorRolesMessage(SocketGuild socketGuild)
        {
            SocketTextChannel? textRolesChannel = socketGuild.TextChannels.FirstOrDefault(x => x.Id == jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Id);

            if (await textRolesChannel.GetMessageAsync(jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.Roles.SwitchColor.Id) is IUserMessage sentMessage)
            {
                await sentMessage.ModifyAsync(message =>
                {
                    message.Embed = extensionEmbedMessage.GetSwitchColorEmbedMessage();
                    message.Components = extensionSelectionMenu.GetColorSwitchSelectionMenu();
                });
            }
            else
            {
                await textRolesChannel.SendMessageAsync(embed: extensionEmbedMessage.GetSwitchColorEmbedMessage(), components: extensionSelectionMenu.GetColorSwitchSelectionMenu());
            }
        }
        private async Task SendRulesMessage(SocketGuild socketGuild)
        {
            SocketTextChannel? textRulesChannel = socketGuild.TextChannels.FirstOrDefault(x => x.Id == jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Rules.Id);

            if (await textRulesChannel.GetMessageAsync(jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.Roles.Rules.Id) is IUserMessage sentMessage)
            {
                await sentMessage.ModifyAsync(message =>
                {
                    message.Embed = extensionEmbedMessage.GetRulesEmbedMessage();
                });
            }
            else
            {
                await textRulesChannel.SendMessageAsync(embed: extensionEmbedMessage.GetRulesEmbedMessage());
            }
        }
        #endregion

        #region Public
        public async Task SendFarewellMessageAsync(SocketGuild socketGuild, SocketUser socketUser)
        {
            SocketTextChannel? socketTextChannel = socketGuild.GetTextChannel(jsonChannelsMapProvider.RootChannel.Channels.TextChannels.AdministratorCategory.Logs.Id);
            Embed embedMessage = extensionEmbedMessage.GetFarewellEmbedTamplate(socketUser);

            if (socketTextChannel == null) { return; }

            await socketTextChannel.SendMessageAsync(embed: embedMessage);
        }
        public async Task SendWelcomeMessageAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                string auCode = AutorizationManager.GetAutorizationCode();
                autorizationCache.SetTemporaryCodes(socketGuildUser, auCode);

                SocketTextChannel? textChannel = socketGuildUser.Guild.TextChannels.FirstOrDefault(x => x.Id == channelsProvider.RootChannel?.Channels?.TextChannels?.ServerCategory?.Starting?.Id);
                MessageComponent auMessageComponent = ExtensionMessageComponents.GetWelcomeMessageComponent(socketGuildUser.Id);
                Embed embedMessage = extensionEmbedMessage.GetJoinedEmbedTemplate(socketGuildUser, auCode);

                if (textChannel is null)
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
            await modal.FollowupAsync(embed: extensionEmbedMessage.GetSuccesAuthorizationMessageEmbedTemplate(),
                components: ExtensionMessageComponents.GetAdditionalWelcomeMessageComponent(modal.User.Id),
                ephemeral: true);
        }
        public async Task SendFollowupMessageOnErrorAutorization(SocketModal modal)
        {
            GuildEmote? emoteError = emotesCache.GetEmote(emotesProvider.RootDiscordEmotes.StaticEmotes.StaticZero.Hmph.Id);

            await modal.FollowupAsync(embed: extensionEmbedMessage.GetErrorAuthorizationMessageEmbedTemplate(emoteError), ephemeral: true);
        }
        public async Task SendMemberInformation(SocketGuildUser socketGuildUser)
        {
            Embed memberInformationEmbed = extensionEmbedMessage.GetGuildUserInformationMessageTemplate(socketGuildUser);
            SocketTextChannel adminTextChannel = socketGuildUser.Guild.GetTextChannel(jsonChannelsMapProvider.RootChannel.Channels.TextChannels.AdministratorCategory.Logs.Id);

            await adminTextChannel.SendMessageAsync(embed: memberInformationEmbed);
        }
        #endregion
    }
}
