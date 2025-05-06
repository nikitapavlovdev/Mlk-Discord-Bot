using MediatR;
using Discord_Bot.Core.Managers.RolesManagers;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;

namespace Discord_Bot.Core.Notifications.UserJoined
{
    class UserJoinedNotificationHandler(
        ILogger<UserJoinedNotificationHandler> logger,
        RolesManager rolesManager,
        TextMessageManager textMessageManager) : INotificationHandler<UserJoinedNotification>
    {
        public async Task Handle(UserJoinedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await rolesManager.AddNotRegisteredRoleAsync(notification.SocketGuildUser);
                await textMessageManager.SendWelcomeMessageAsync(notification.SocketGuildUser);
                await textMessageManager.SendMemberInformation(notification.SocketGuildUser);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}