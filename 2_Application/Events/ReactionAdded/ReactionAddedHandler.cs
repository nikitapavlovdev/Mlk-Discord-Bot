using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.UserManagers;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Events.ReactionAdded
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
                if (notification.Message.Id == jsonDiscordDynamicMessagesProvider.AuMessageId)
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
