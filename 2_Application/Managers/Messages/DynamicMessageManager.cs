using Discord.WebSocket;
using Discord;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.DTOs;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Utilities.DI;

namespace MlkAdmin._2_Application.Managers.Messages
{
    public class DynamicMessageManager(
        ILogger<DynamicMessageManager> logger,
        DiscordSocketClient client,
        EmbedMessageExtension embedExtension,
        JsonChannelsMapProvider jsonChannelsMapProvider,
        JsonDiscordDynamicMessagesProvider jsonDiscordDynamicMessagesProvider) : IMessageUpdater
    {
        public async Task SendOrUpdateAsync(DynamicMessageDto dto)
        {
            try
            {
                SocketGuild guild = client.GetGuild(dto.GuildId);
                SocketTextChannel? channel = guild.TextChannels.FirstOrDefault(x => x.Id == dto.ChannelId);

                if (await channel.GetMessageAsync(dto.MessageId) is IUserMessage sentMessage)
                {
                    await sentMessage.ModifyAsync(message =>
                    {
                        message.Embed = embedExtension.GetMainRolesEmbedMessage();
                    });
                }
                else
                {
                    await channel.SendMessageAsync(embed: embedExtension.GetMainRolesEmbedMessage());
                } 
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}", ex.Message);
            }
        }

        private async Task SendMainGuildRolesMessage(SocketGuild socketGuild)
        {
            await SendOrUpdateAsync(new DynamicMessageDto()
            {
                GuildId = socketGuild.Id,
                ChannelId = jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Id,
                MessageId = jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.Roles.MainRoles.Id,
            });
        }
    }
}
