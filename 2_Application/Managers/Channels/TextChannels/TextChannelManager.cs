using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers
{
    public class TextChannelManager(
        ILogger<TextChannelManager> logger,
        EmbedMessageExtension embedMessageExtension,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider)
    {
        #region Public
        public async Task SendWelcomeMessageAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                SocketTextChannel? textChannel = socketGuildUser.Guild.TextChannels.FirstOrDefault(x => x.Id == jsonChannelsMapProvider.StartingChannelId);

                if (textChannel is null) { return; }

                Embed embedMessage = embedMessageExtension.CreateEmbed(new()
                {
                    Title = "ᴍᴀʟᴇɴᴋɪᴇ ɴᴇᴡ ᴍᴇᴍʙᴇʀ",
                    Description = $"Привет, **{socketGuildUser.Username}**!" +
                        $"\nДобро пожаловать на сервер **{socketGuildUser.Guild.Name}**\n\n" +
                        $"Для продолжения проследуйте в {jsonChannelsMapProvider.HubChannelHttps}",
                    Color = new Color(30, 144, 255),
                    FooterDto = new() { 
                        Text = socketGuildUser.DisplayName, 
                        IconUrl = socketGuildUser.GetAvatarUrl(ImageFormat.Auto, 48) 
                    },
                    WithTimestamp = true
                });

                await textChannel.SendMessageAsync($"{socketGuildUser.Mention}", embed: embedMessage, components: MessageComponentsExtension.GetServerHubLinkButton(jsonChannelsMapProvider.HubChannelHttps));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        
        #endregion
    }
}
