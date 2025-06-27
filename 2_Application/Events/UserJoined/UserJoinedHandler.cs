using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.RolesManagers;
using MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers;

namespace MlkAdmin._2_Application.Notifications.UserJoined
{
    class UserJoinedHandler(
        ILogger<UserJoinedHandler> logger,
        RolesManager rolesManager,
        TextMessageManager textMessageManager) : INotificationHandler<UserJoined>
    {
        public async Task Handle(UserJoined notification, CancellationToken cancellationToken)
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