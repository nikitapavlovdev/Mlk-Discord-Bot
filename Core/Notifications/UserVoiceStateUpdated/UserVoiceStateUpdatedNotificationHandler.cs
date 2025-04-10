using MediatR;
using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cash;
using Discord.Rest;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Core.Notifications.UserVoiceStateUpdated
{
    class UserVoiceStateUpdatedNotificationHandler(ChannelsCash channelsCash, ILogger<UserVoiceStateUpdatedNotificationHandler> _logger) : INotificationHandler<UserVoiceStateUpdatedNotification>
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
                    if (channelsCash.IsTemporaryChannel(notification.OldState.VoiceChannel.Id) && notification.OldState.VoiceChannel.ConnectedUsers.Count == 0)
                    {
                        channelsCash.DeleteTemoraryChannelFromList(notification.OldState.VoiceChannel);
                        await notification.OldState.VoiceChannel.DeleteAsync();
                    }
                }

                if (notification.OldState.VoiceChannel == null && notification.NewState.VoiceChannel != null)
                {
                    if (!channelsCash.IsGeneratingChannel(notification.NewState.VoiceChannel.Id))
                    {
                        return;
                    }

                    RestVoiceChannel newChannel = await notification.NewState.VoiceChannel.Guild.CreateVoiceChannelAsync(
                                                "ᴍʟᴋʟᴏʙʙʏ: ",
                                                properties => 
                                                properties.CategoryId = notification.NewState.VoiceChannel.CategoryId
                    );

                    channelsCash.AddTemporaryChannelInList(newChannel);
                    await guildUser.ModifyAsync(properties => properties.ChannelId = newChannel.Id);
                }
                
                if (notification.OldState.VoiceChannel != null && notification.NewState.VoiceChannel != null)
                {
                    if (channelsCash.IsTemporaryChannel(notification.OldState.VoiceChannel.Id) && notification.OldState.VoiceChannel.ConnectedUsers.Count == 0)
                    {
                        channelsCash.DeleteTemoraryChannelFromList(notification.OldState.VoiceChannel);
                        await notification.OldState.VoiceChannel.DeleteAsync();
                    }

                    if (channelsCash.IsGeneratingChannel(notification.NewState.VoiceChannel.Id))
                    {
                        RestVoiceChannel newChannel = await notification.NewState.VoiceChannel.Guild.CreateVoiceChannelAsync(
                                                "ᴍʟᴋʟᴏʙʙʏ: ",
                                                properties =>
                                                properties.CategoryId = notification.NewState.VoiceChannel.CategoryId
                        );

                        channelsCash.AddTemporaryChannelInList(newChannel);
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