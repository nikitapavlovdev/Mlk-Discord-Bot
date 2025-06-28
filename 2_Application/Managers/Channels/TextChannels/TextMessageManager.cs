using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Discord;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin.Infrastructure.Cache;
using MlkAdmin.Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers
{
    public class TextMessageManager(ILogger<TextMessageManager> logger, 
        EmbedMessageExtension extensionEmbedMessage,
        JsonDiscordChannelsMapProvider channelsProvider,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider,
        ChannelsCache channelsCache,       
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider,
        DiscordSocketClient client)
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
                SocketTextChannel? textChannel = socketGuildUser.Guild.TextChannels.FirstOrDefault(x => x.Id == channelsProvider.RootChannel?.Channels?.TextChannels?.ServerCategory?.Starting?.Id);
                Embed embedMessage = extensionEmbedMessage.GetJoinedEmbedTemplate(socketGuildUser);

                if (textChannel is null)
                {
                    return;
                }

                await textChannel.SendMessageAsync($"{socketGuildUser.Mention}", embed: embedMessage, components: MessageComponentsExtension.GetServerHubLinkButton(channelsProvider.RootChannel.Channels.TextChannels.ServerCategory.Hub.Https));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public async Task SendUserInputToDeveloper(SocketModal modal, string title, string buttonName, string input_text_lable_1, string input_text_lable_2 = "")
        {
            try
            {
                SocketGuild guild = client.GetGuild(jsonDiscordConfigurationProvider.RootDiscordConfiguration.Guild.Id);
                SocketTextChannel channel = guild.GetTextChannel(jsonChannelsMapProvider.RootChannel.Channels.TextChannels.AdministratorCategory.Feedback.Id);

                string descriptions = $"**Данные из формы**: {buttonName}\n\n" +
                    "input_1: \n> " + input_text_lable_1 + "\n\n";

                if (!string.IsNullOrEmpty(input_text_lable_2))
                {
                    descriptions += $"input_2: \n> " + input_text_lable_2 + "\n\n";
                }

                await channel.SendMessageAsync(embed: extensionEmbedMessage.GetUserChoiceEmbedTamplate(modal.User, title, descriptions));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        #endregion
    }
}
