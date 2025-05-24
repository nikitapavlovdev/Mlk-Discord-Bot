using Discord.WebSocket;
using MlkAdmin.Core.Utilities.DI;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin.Core.Notifications.ButtonExecuted
{
    public class ButtonExecutedNotificationHandler(
        ILogger<ButtonExecutedNotificationHandler> _logger) : INotificationHandler<ButtonExecutedNotification>
    {
        public async Task Handle(ButtonExecutedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                if(notification.SocketMessageComponent.User is not SocketGuildUser socketGuildUser)
                {
                    return;
                }

                if(notification.SocketMessageComponent.Data.CustomId == $"personal_data_button")
                {
                    await notification.SocketMessageComponent.RespondWithModalAsync(ExtensionModal.GetPersonalInformationModal());
                    return;
                }

                if(notification.SocketMessageComponent.Data.CustomId == "autolobby_naming_button")
                {
                    await notification.SocketMessageComponent.RespondWithModalAsync(ExtensionModal.GetLobbyNamingModal());
                    return;
                }

                if(notification.SocketMessageComponent.Data.CustomId == "feedback_button")
                {
                    await notification.SocketMessageComponent.RespondWithModalAsync(ExtensionModal.GetFeedBackModal());
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
