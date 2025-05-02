 using MediatR;
using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cache;
using Discord.Rest;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.ChannelsManagers.VoiceChannelsManagers;

namespace Discord_Bot.Core.Notifications.UserVoiceStateUpdated
{
    class UserVoiceStateUpdatedNotificationHandler(
        ChannelsCache channelsCache, 
        ILogger<UserVoiceStateUpdatedNotificationHandler> logger,
        VoiceChannelsManager voiceChannelsCreator) : INotificationHandler<UserVoiceStateUpdatedNotification>
    {
        public async Task Handle(UserVoiceStateUpdatedNotification notification, CancellationToken cancellationToken)
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