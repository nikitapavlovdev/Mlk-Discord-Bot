using Discord.WebSocket;
using MlkAdmin.Core.Utilities.DI;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Notifications.ButtonExecuted
{
    public class ButtonExecutedHandler(
        ILogger<ButtonExecutedHandler> _logger) : INotificationHandler<ButtonExecuted>
    {
        public async Task Handle(ButtonExecuted notification, CancellationToken cancellationToken)
        {
            try
            {
                if(notification.SocketMessageComponent.User is not SocketGuildUser socketGuildUser)
                {
                    return;
                }

                if(notification.SocketMessageComponent.Data.CustomId == $"personal_data_button")
                {
                    await notification.SocketMessageComponent.RespondWithModalAsync(ModalExtension.GetPersonalInformationModal());
                    return;
                }

                if(notification.SocketMessageComponent.Data.CustomId == "autolobby_naming_button")
                {
                    await notification.SocketMessageComponent.RespondWithModalAsync(ModalExtension.GetLobbyNamingModal());
                    return;
                }

                if(notification.SocketMessageComponent.Data.CustomId == "feedback_button")
                {
                    await notification.SocketMessageComponent.RespondWithModalAsync(ModalExtension.GetFeedBackModal());
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
