using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Discord;
using MlkAdmin.Core.Utilities.DI;
using MlkAdmin.Infrastructure.Cache;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using Discord.Rest;

namespace MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers
{
    public class TextMessageManager(ILogger<TextMessageManager> logger, 
        EmbedMessageExtension extensionEmbedMessage,
        JsonChannelsMapProvider channelsProvider,
        JsonChannelsMapProvider jsonChannelsMapProvider,
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

                await textChannel.SendMessageAsync($"{socketGuildUser.Mention}", embed: embedMessage);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public async Task SendUserInputToDeveloper(SocketModal modal, string title, string buttonName, string input_text1)
        {
            try
            {
                SocketGuild guild = client.GetGuild(jsonDiscordConfigurationProvider.RootDiscordConfiguration.Guild.Id);
                SocketTextChannel channel = guild.GetTextChannel(jsonChannelsMapProvider.RootChannel.Channels.TextChannels.AdministratorCategory.Logs.Id);

                string descriptions = $"**Данные из формы**: {buttonName}\n" +
                    $"**Введенные данные**: \n\n" +
                    "text_input 1: " + "```" + input_text1 + "```" + "\n";

                await channel.SendMessageAsync(embed: extensionEmbedMessage.GetUserChoiceEmbedTamplate(modal.User, title, descriptions));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }

        }
        public async Task SendUserInputToDeveloper(SocketModal modal, string title, string buttonName, string input_text1, string input_text2)
        {
            try
            {
                SocketGuild guild = client.GetGuild(jsonDiscordConfigurationProvider.RootDiscordConfiguration.Guild.Id);
                SocketTextChannel channel = guild.GetTextChannel(jsonChannelsMapProvider.RootChannel.Channels.TextChannels.AdministratorCategory.Logs.Id);

                string descriptions = $"**Данные из формы**: {buttonName}\n" +
                    $"**Введенные данные**: \n\n" +
                    "text_input 1: " + input_text1 + "\n" +
                    "text_input 2: " + input_text2;

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
