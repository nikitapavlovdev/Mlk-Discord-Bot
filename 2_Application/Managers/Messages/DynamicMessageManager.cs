using Discord.WebSocket;
using Discord;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.DTOs;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._2_Application.Managers.Components;

namespace MlkAdmin._2_Application.Managers.Messages
{
    public class DynamicMessageManager(
        ILogger<DynamicMessageManager> logger,
        IEmbedDtoCreator embedDtoCreator,
        DiscordSocketClient client,
        EmbedMessageExtension embedExtension,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider,
        JsonDiscordDynamicMessagesProvider jsonDiscordDynamicMessagesProvider,
        ComponentsManager componentsManager) : IDynamicMessageCenter
    {
        public async Task UpdateAllDM(ulong guildId)
        {
            await Task.WhenAll(
                SendMessageWithAutorization(guildId),
                SendMessageWithFeatures(guildId),
                SendMessageWithRules(guildId),
                SendMessageWithGuildRoles(guildId),
                SendMessageWithNameColor(guildId));
        }

        private async Task SendOrUpdateAsync(DynamicMessageDto DMdto, EmbedDto embedDto, MessageComponent? messageComponent = null)
        {
            try
            {
                SocketGuild guild = client.GetGuild(DMdto.GuildId);
                SocketTextChannel? channel = guild.TextChannels.FirstOrDefault(x => x.Id == DMdto.ChannelId);

                if (await channel.GetMessageAsync(DMdto.MessageId) is IUserMessage sentMessage)
                {
                    await sentMessage.ModifyAsync(message =>
                    {
                        message.Embed = embedExtension.GetDynamicMessageEmbedTamplate(embedDto);
                        message.Components = messageComponent;
                    });
                }
                else
                {
                    await channel.SendMessageAsync(embed: embedExtension.GetDynamicMessageEmbedTamplate(embedDto), components: messageComponent);
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
                ChannelId = jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Hub.Id,
                MessageId = jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.ServerHub.Autorization.Id,
            },
            await embedDtoCreator.GetEmbedDto(DynamicMessageType.Authorization));
        }

        private async Task SendMessageWithFeatures(ulong guildId)
        {
            await SendOrUpdateAsync(new DynamicMessageDto()
            {
                GuildId = guildId,
                ChannelId = jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Hub.Id,
                MessageId = jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.ServerHub.Features.Id,
            },
           await embedDtoCreator.GetEmbedDto(DynamicMessageType.Features), await componentsManager.GetMessageComponent(DynamicMessageType.Features));
        }

        private async Task SendMessageWithRules(ulong guildId)
        {
            await SendOrUpdateAsync(new DynamicMessageDto()
            {
                GuildId = guildId,
                ChannelId = jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Rules.Id,
                MessageId = jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.Rules.Id,
            },
           await embedDtoCreator.GetEmbedDto(DynamicMessageType.Rules));
        }

        private async Task SendMessageWithGuildRoles(ulong guildId)
        {
            await SendOrUpdateAsync(new DynamicMessageDto()
            {
                GuildId = guildId,
                ChannelId = jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Id,
                MessageId = jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.Roles.Main.Id,
            },
            await embedDtoCreator.GetEmbedDto(DynamicMessageType.Roles));
        }

        private async Task SendMessageWithNameColor(ulong guildId)
        {
            await SendOrUpdateAsync(new DynamicMessageDto()
            {
                GuildId = guildId,
                ChannelId = jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Id,
                MessageId = jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.Roles.NameColor.Id,
            },
            await embedDtoCreator.GetEmbedDto(DynamicMessageType.NameColor), await componentsManager.GetMessageComponent(DynamicMessageType.NameColor));
        }
    }
}