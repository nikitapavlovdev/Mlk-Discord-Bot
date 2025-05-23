using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Managers.UserManagers;
using MlkAdmin.Core.Providers.JsonProvider;

namespace MlkAdmin.Core.Notifications.ReactionAdded
{
    public class ReactionAddedNotificationHandler(
        ILogger<ReactionAddedNotificationHandler> logger,
        JsonDiscordDynamicMessagesProvider jsonDiscordDynamicMessagesProvider,
        AutorizationManager autorizationManager) : INotificationHandler<ReactionAddedNotification>
    {
        public async Task Handle(ReactionAddedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.Message.Id == jsonDiscordDynamicMessagesProvider.DynamicMessages.Messages.ServerHub.Autorization.Id)
                {
                    await autorizationManager.AuthorizeUser(notification.Reaction.UserId);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
