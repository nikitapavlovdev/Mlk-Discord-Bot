using MediatR;
using Discord.WebSocket;
using MlkAdmin.Infrastructure.Cache;
using Discord.Rest;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers;

namespace MlkAdmin._2_Application.Notifications.UserVoiceStateUpdated
{
    class UserVoiceStateUpdatedHandler(
        ChannelsCache channelsCache, 
        ILogger<UserVoiceStateUpdatedHandler> logger,
        VoiceChannelsManager voiceChannelsCreator) : INotificationHandler<UserVoiceStateUpdated>
    {
        public async Task Handle(UserVoiceStateUpdated notification, CancellationToken cancellaChannelsManagerstionToken)
        {
            try
            {
                if (notification.SocketUser is not SocketGuildUser guildUser)
                {
                    return;
                }

                if (notification.OldState.VoiceChannel != null && notification.NewState.VoiceChannel == null)
                {
                    if (channelsCache.IsTemporaryChannel(notification.OldState.VoiceChannel) && notification.OldState.VoiceChannel.ConnectedUsers.Count == 0)
                    {
                        channelsCache.DeleteTemporaryChannel(notification.OldState.VoiceChannel);

                        await notification.OldState.VoiceChannel.DeleteAsync();
                    }
                }

                if (notification.OldState.VoiceChannel == null && notification.NewState.VoiceChannel != null)
                {
                    if (!channelsCache.IsGeneratingChannel(notification.NewState.VoiceChannel))
                    {
                        return;
                    }

                    RestVoiceChannel brandNewRestChannel = await voiceChannelsCreator.CreateVoiceChannelAsync(notification.NewState.VoiceChannel.Guild, notification.SocketUser);
                    channelsCache.AddTemporaryChannel(brandNewRestChannel);

                    await guildUser.ModifyAsync(properties => properties.ChannelId = brandNewRestChannel.Id);
                }
                
                if (notification.OldState.VoiceChannel != null && notification.NewState.VoiceChannel != null)
                {
                    if (channelsCache.IsTemporaryChannel(notification.OldState.VoiceChannel) && notification.OldState.VoiceChannel.ConnectedUsers.Count == 0)
                    {
                        channelsCache.DeleteTemporaryChannel(notification.OldState.VoiceChannel);

                        await notification.OldState.VoiceChannel.DeleteAsync();
                    }

                    if (channelsCache.IsGeneratingChannel(notification.NewState.VoiceChannel))
                    {
                        RestVoiceChannel brandNewRestChannel = await voiceChannelsCreator.CreateVoiceChannelAsync(notification.NewState.VoiceChannel.Guild, notification.SocketUser);
                        channelsCache.AddTemporaryChannel(brandNewRestChannel);

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