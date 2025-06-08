using Discord.WebSocket;
using Discord;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.DTOs;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Utilities.DI;
using MlkAdmin._1_Domain.Enums;

namespace MlkAdmin._2_Application.Managers.Messages
{
    public class DynamicMessageManager(
        ILogger<DynamicMessageManager> logger,
        IEmbedDtoCreator embedDtoCreator,
        DiscordSocketClient client,
        EmbedMessageExtension embedExtension,
        JsonChannelsMapProvider jsonChannelsMapProvider,
        JsonDiscordDynamicMessagesProvider jsonDiscordDynamicMessagesProvider) : IDynamicMessageCenter
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

        private async Task SendOrUpdateAsync(DynamicMessageDto DMdto, EmbedDto embedDto)
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
                    });
                }
                else
                {
                    await channel.SendMessageAsync(embed: embedExtension.GetDynamicMessageEmbedTamplate(embedDto));
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
           await embedDtoCreator.GetEmbedDto(DynamicMessageType.Features));
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
            await embedDtoCreator.GetEmbedDto(DynamicMessageType.NameColor));
        }
    }
}