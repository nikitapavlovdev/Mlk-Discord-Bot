using MediatR;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.UserManagers;

namespace Discord_Bot.Core.Notifications.ModalSubmitted
{
    class ModalSubmittedNotificationHandler(
        ILogger<ModalSubmittedNotificationHandler> _logger,
        AutorizationManager _autorizationManager,
        PersonalDataManager _personalDataManager) : INotificationHandler<ModalSubmittedNotification>
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
                        await _autorizationManager.AuthorizeUser(notification.Modal, socketGuildUser);
                        break;

                    case "personal_data":
                        await _personalDataManager.GetUserPersonalData(notification.Modal);
                        break;

                    default:
                        _logger.LogInformation("Неизвестный CustomId: {CustomId}", notification.Modal.Data.CustomId);
                        break;

                }
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
