using MediatR;
using Discord_Bot.Infrastructure.Cache;
using Microsoft.Extensions.Logging;
namespace Discord_Bot.Core.Notifications.GuildAvailable
{
    class GuildAvailableNotificationHandler(
        ChannelsCache channelsCache,
        RolesCache rolesCache,
        EmotesCache emotesCache,
        ILogger<GuildAvailableNotificationHandler> _logger) : INotificationHandler<GuildAvailableNotification>
    {
        public async Task Handle(GuildAvailableNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.WhenAll(
                        channelsCache.ChannelsInitialization(notification.SocketGuild),
                        rolesCache.RolesInitialization(notification.SocketGuild),
                        emotesCache.EmotesInitialization(notification.SocketGuild)
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
