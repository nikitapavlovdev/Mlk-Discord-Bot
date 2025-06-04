using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin.Application.Managers.ChannelsManagers.TextChannelsManagers;


namespace MlkAdmin.Application.Notifications.UserLeft
{
    class UserLeftHandler(
        ILogger<UserLeftHandler> logger,
        TextMessageManager textMessageManager) : INotificationHandler<UserLeft>
    {
        public async Task Handle(UserLeft notification, CancellationToken cancellationToken)
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
