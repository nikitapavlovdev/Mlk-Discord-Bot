using MediatR;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Managers.UserManagers;
using MlkAdmin.Core.Managers.ChannelsManagers.TextChannelsManagers;

namespace MlkAdmin.Core.Notifications.ModalSubmitted
{
    class ModalSubmittedNotificationHandler(
        ILogger<ModalSubmittedNotificationHandler> logger,
        TextMessageManager textMessageManager) : INotificationHandler<ModalSubmittedNotification>
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
                    case "personal_data_modal":

                        await textMessageManager.SendUserInputToDeveloper(
                            notification.Modal, 
                            "Персональные данные", 
                            notification.Modal.Data.Components.FirstOrDefault(x => x.CustomId == "personal_data_input_name").Value, 
                            notification.Modal.Data.Components.FirstOrDefault(x => x.CustomId == "personal_data_input_dateofbirthday").Value);
                        break;

                    case "lobby_naming_modal":
                        await textMessageManager.SendUserInputToDeveloper(
                            notification.Modal, 
                            "Личное имя комнаты", 
                            notification.Modal.Data.Components.FirstOrDefault(x => x.CustomId == "lobby_naming_input_name").Value);
                        break;

                    case "feedback_modal":
                        await textMessageManager.SendUserInputToDeveloper(
                            notification.Modal,
                            "Разраб делай",
                            notification.Modal.Data.Components.FirstOrDefault(x => x.CustomId == "feedback_input_feedback").Value);
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
