using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin.Application.Managers.RolesManagers;
using MlkAdmin.Application.Managers.ChannelsManagers.TextChannelsManagers;

namespace MlkAdmin.Application.Notifications.UserJoined
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