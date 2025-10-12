using MediatR;
using Discord.WebSocket;
using Discord.Rest;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._2_Application.Managers.Users.Stat;

namespace MlkAdmin._2_Application.Events.UserVoiceStateUpdated
{
    class UserVoiceStateUpdatedHandler(
        ILogger<UserVoiceStateUpdatedHandler> logger,
        IVoiceChannelRepository voiceChannelRepository,
        UserStatManager userStatManager,
        VoiceChannelsService voiceChannelsCreator) : INotificationHandler<UserVoiceStateUpdated>
    {
        public async Task Handle(UserVoiceStateUpdated notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.SocketUser is not SocketGuildUser guildUser)
                {
                    return;
                }

                await userStatManager.TrackUserVoiceSessionsAsync(guildUser.Id, notification.NewState, notification.OldState);

                if (notification.OldState.VoiceChannel != null)
                {
                    if(await voiceChannelRepository.IsTemporaryVoiceChannel(notification.OldState.VoiceChannel.Id) && notification.OldState.VoiceChannel.ConnectedUsers.Count == 0)
                    {
                        await voiceChannelRepository.RemoveDbVoiceChannelAsync(notification.OldState.VoiceChannel.Id);
                        await notification.OldState.VoiceChannel.DeleteAsync();
                    }
                }

                if (notification.NewState.VoiceChannel != null)
                {
                    if (await voiceChannelRepository.IsGeneratingVoiceChannel(notification.NewState.VoiceChannel.Id))
                    {
                        RestVoiceChannel brandNewRestChannel = await voiceChannelsCreator.CreateVoiceChannelAsync(notification.NewState.VoiceChannel.Guild, notification.SocketUser);

                        VoiceChannel dbChannel = new()
                        {
                            Category = notification.NewState.VoiceChannel.Category.ToString(),
                            Id = brandNewRestChannel.Id,
                            ChannelName = brandNewRestChannel.Name,
                            IsGenerating = false,
                            IsTemporary = true
                        };

                        await voiceChannelRepository.UpsertDbVoiceChannelAsync(dbChannel);
                        await guildUser.ModifyAsync(properties => properties.ChannelId = brandNewRestChannel.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}