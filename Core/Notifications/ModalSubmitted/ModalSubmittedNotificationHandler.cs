using MediatR;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Managers.UserManagers;

namespace MlkAdmin.Core.Notifications.ModalSubmitted
{
    class ModalSubmittedNotificationHandler(
        ILogger<ModalSubmittedNotificationHandler> logger,
        AutorizationManager autorizationManager,
        PersonalDataManager personalDataManager) : INotificationHandler<ModalSubmittedNotification>
    {
        public async Task Handle(ModalSubmittedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await notification.Modal.DeferAsync();

                if (notification.Modal.User is not SocketGuildUser socketGuildUser)
                {
                    return;
                }

                switch(notification.Modal.Data.CustomId)
                {
                    case "au_selection":
                        await autorizationManager.AuthorizeUser(notification.Modal, socketGuildUser);
                        break;

                    case "personal_data":
                        await personalDataManager.GetUserPersonalData(notification.Modal);
                        break;

                    default:
                        logger.LogInformation("Неизвестный CustomId: {CustomId}", notification.Modal.Data.CustomId);
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
