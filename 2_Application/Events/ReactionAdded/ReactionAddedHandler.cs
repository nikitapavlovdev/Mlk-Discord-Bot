using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.UserManagers;
using MlkAdmin.Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Notifications.ReactionAdded
{
    public class ReactionAddedHandler(
        ILogger<ReactionAddedHandler> logger,
        JsonDiscordDynamicMessagesProvider jsonDiscordDynamicMessagesProvider,
        AutorizationManager autorizationManager) : INotificationHandler<ReactionAdded>
    {
        public async Task Handle(ReactionAdded notification, CancellationToken cancellationToken)
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
