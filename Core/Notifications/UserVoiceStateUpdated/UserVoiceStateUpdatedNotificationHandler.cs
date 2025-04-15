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
        ILogger<UserVoiceStateUpdatedNotificationHandler> _logger,
        VoiceChannelsCreator voiceChannelsCreator) : INotificationHandler<UserVoiceStateUpdatedNotification>
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
                    if (channelsCache.IsTemporaryChannel(notification.OldState.VoiceChannel.Id) && notification.OldState.VoiceChannel.ConnectedUsers.Count == 0)
                    {
                        channelsCache.DeleteTemoraryChannelFromList(notification.OldState.VoiceChannel);
                        await notification.OldState.VoiceChannel.DeleteAsync();
                    }
                }

                if (notification.OldState.VoiceChannel == null && notification.NewState.VoiceChannel != null)
                {
                    if (!channelsCache.IsGeneratingChannel(notification.NewState.VoiceChannel.Id))
                    {
                        return;
                    }

                    RestVoiceChannel newChannel = await voiceChannelsCreator.CreateVoiceChannelAsync(notification.NewState.VoiceChannel.Guild, notification.NewState.VoiceChannel, notification.SocketUser);

                    channelsCache.AddTemporaryChannelInList(newChannel);
                    await guildUser.ModifyAsync(properties => properties.ChannelId = newChannel.Id);
                }
                
                if (notification.OldState.VoiceChannel != null && notification.NewState.VoiceChannel != null)
                {
                    if (channelsCache.IsTemporaryChannel(notification.OldState.VoiceChannel.Id) && notification.OldState.VoiceChannel.ConnectedUsers.Count == 0)
                    {
                        channelsCache.DeleteTemoraryChannelFromList(notification.OldState.VoiceChannel);
                        await notification.OldState.VoiceChannel.DeleteAsync();
                    }

                    if (channelsCache.IsGeneratingChannel(notification.NewState.VoiceChannel.Id))
                    {
                        RestVoiceChannel newChannel = await voiceChannelsCreator.CreateVoiceChannelAsync(notification.NewState.VoiceChannel.Guild, notification.NewState.VoiceChannel, notification.SocketUser);

                        channelsCache.AddTemporaryChannelInList(newChannel);
                        await guildUser.ModifyAsync(properties => properties.ChannelId = newChannel.Id);
                    }
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}