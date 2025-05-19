using MlkAdmin.Core.Managers.ChannelsManagers.TextChannelsManagers;
using MediatR;
using Microsoft.Extensions.Logging;


namespace MlkAdmin.Core.Notifications.UserLeft
{
    class UserLeftNotificationHandler(
        ILogger<UserLeftNotificationHandler> logger,
        TextMessageManager textMessageManager) : INotificationHandler<UserLeftNotification>
    {
        public async Task Handle(UserLeftNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await textMessageManager.SendFarewellMessageAsync(notification.SocketGuild, notification.SocketUser);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
