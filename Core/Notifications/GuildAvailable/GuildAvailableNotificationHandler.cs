using MediatR;
using Discord_Bot.Infrastructure.Cash;
using Microsoft.Extensions.Logging;
namespace Discord_Bot.Core.Notifications.GuildAvailable
{
    class GuildAvailableNotificationHandler(
        ChannelsCash channelsCash,
        RolesCash rolesCash,
        EmotesCash emotesCash,
        ILogger<GuildAvailableNotificationHandler> _logger) : INotificationHandler<GuildAvailableNotification>
    {
        public async Task Handle(GuildAvailableNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.WhenAll(
                        channelsCash.ChannelsInitialization(notification.SocketGuild),
                        rolesCash.RolesInitialization(notification.SocketGuild),
                        emotesCash.EmotesInitialization(notification.SocketGuild)
                );

                _logger.LogInformation("All entities have been initialized");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
