using MediatR;
using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cash;
using Discord.Rest;

namespace Discord_Bot.Core.Notifications.UserVoiceStateUpdated
{
    class UserVoiceStateUpdatedNotificationHandler(ChannelsCash channelsCash) : INotificationHandler<UserVoiceStateUpdatedNotification>
    {
        private readonly ChannelsCash channelsCash = channelsCash;

        public async Task Handle(UserVoiceStateUpdatedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                SocketGuildUser guildUser = notification.SocketUser as SocketGuildUser;

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
                                                "mlklobby",
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
                                                "mlklobby",
                                                properties =>
                                                properties.CategoryId = notification.NewState.VoiceChannel.CategoryId
                        );

                        channelsCash.AddTemporaryChannelInList(newChannel);
                        await guildUser.ModifyAsync(properties => properties.ChannelId = newChannel.Id);
                    }
                }
               
            }
            catch (Exception)
            {
                Console.WriteLine("Что то не так");
            }
        }
    }
}