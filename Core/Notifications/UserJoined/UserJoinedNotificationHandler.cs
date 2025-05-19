using MediatR;
using MlkAdmin.Core.Managers.RolesManagers;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Managers.ChannelsManagers.TextChannelsManagers;

namespace MlkAdmin.Core.Notifications.UserJoined
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
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}