using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.DTOs.Messages;
using MlkAdmin._2_Application.DTOs.Embed;
using MlkAdmin._2_Application.Managers.Components;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using MlkAdmin._3_Infrastructure.Discord.Extensions;

namespace MlkAdmin._2_Application.Managers.Messages
{
    public class DynamicMessageManager(
        ILogger<DynamicMessageManager> logger,
        IEmbedDtoCreator embedDtoCreator,
        EmbedMessageExtension embedMessageExtension,
        DiscordSocketClient client,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider,
        JsonDiscordDynamicMessagesProvider jsonDiscordDynamicMessagesProvider,
        ComponentsManager componentsManager) : IDynamicMessageCenter
    {
        public async Task UpdateAllDM(ulong guildId)
        {
            await Task.WhenAll(
                SendMessageWithAutorization(guildId),
                SendMessageWithRules(guildId),
                SendMessageWithGuildRoles(guildId),
                SendMessageWithNameColor(guildId));
        }

        private async Task SendOrUpdateAsync(DynamicMessageDto DMDto, EmbedDto embedDto, MessageComponent? messageComponent = null)
        {
            try
            {
                SocketGuild guild = client.GetGuild(DMDto.GuildId);
                SocketTextChannel? channel = guild.TextChannels.FirstOrDefault(x => x.Id == DMDto.ChannelId);

                if (await channel.GetMessageAsync(DMDto.MessageId) is IUserMessage sentMessage)
                {
                    await sentMessage.ModifyAsync(message =>
                    {
                        message.Embed = embedMessageExtension.CreateEmbed(embedDto);
                        message.Components = messageComponent;
                    });
                }
                else
                {
                    await channel.SendMessageAsync(embed: embedMessageExtension.CreateEmbed(embedDto), components: messageComponent);
                } 
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}", ex.Message);
            }
        }
        private async Task SendMessageWithAutorization(ulong guildId)
        {
            await SendOrUpdateAsync(new DynamicMessageDto()
            {
                GuildId = guildId,
                ChannelId = jsonChannelsMapProvider.HubChannelId,
                MessageId = jsonDiscordDynamicMessagesProvider.AuMessageId,
            },
            await embedDtoCreator.GetEmbedDto(DynamicMessageType.Authorization));
        }
        private async Task SendMessageWithRules(ulong guildId)
        {
            await SendOrUpdateAsync(new DynamicMessageDto()
            {
                GuildId = guildId,
                ChannelId = jsonChannelsMapProvider.RulesChannelId,
                MessageId = jsonDiscordDynamicMessagesProvider.RulesMessageId,
            },
           await embedDtoCreator.GetEmbedDto(DynamicMessageType.Rules));
        }
        private async Task SendMessageWithGuildRoles(ulong guildId)
        {
            await SendOrUpdateAsync(new DynamicMessageDto()
            {
                GuildId = guildId,
                ChannelId = jsonChannelsMapProvider.RolesChannelId,
                MessageId = jsonDiscordDynamicMessagesProvider.MainRolesMessageId,
            },
            await embedDtoCreator.GetEmbedDto(DynamicMessageType.Roles));
        }
        private async Task SendMessageWithNameColor(ulong guildId)
        {
            await SendOrUpdateAsync(new DynamicMessageDto()
            {
                GuildId = guildId,
                ChannelId = jsonChannelsMapProvider.RolesChannelId,
                MessageId = jsonDiscordDynamicMessagesProvider.NameColorRolesMessageId,
            },
            await embedDtoCreator.GetEmbedDto(DynamicMessageType.NameColor), 
            await componentsManager.CreateMessageComponent(DynamicMessageType.NameColor));
        }
    }
}