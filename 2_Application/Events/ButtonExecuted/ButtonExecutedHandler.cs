using Discord.WebSocket;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Notifications.ButtonExecuted
{
    public class ButtonExecutedHandler(
        ILogger<ButtonExecutedHandler> logger) : INotificationHandler<ButtonExecuted>
    {
        public async Task Handle(ButtonExecuted notification, CancellationToken cancellationToken)
        {
            try
            {
                if(notification.SocketMessageComponent.User is not SocketGuildUser socketGuildUser)
                {
                    return;
                }

                switch (notification.SocketMessageComponent.Data.CustomId)
                {
                    case "personal_data_button":
                        await notification.SocketMessageComponent.RespondWithModalAsync(ModalExtension.GetPersonalInformationModal());
                        return;

                    case "autolobby_naming_button":
                        await notification.SocketMessageComponent.RespondWithModalAsync(ModalExtension.GetLobbyNamingModal());
                        return;

                    case "feedback_button":
                        await notification.SocketMessageComponent.RespondWithModalAsync(ModalExtension.GetFeedBackModal());
                        return;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
