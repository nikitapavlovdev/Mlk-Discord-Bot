using Discord_Bot.Core.Utilities.DI;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Core.Notifications.ButtonExecuted
{
    public class ButtonExecutedNotificationHandler(ILogger<ButtonExecutedNotificationHandler> _logger) : INotificationHandler<ButtonExecutedNotification>
    {
        public async Task Handle(ButtonExecutedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.SocketMessageComponent.Data.CustomId == $"nikname_selection_component_{notification.SocketMessageComponent.User.Id}")
                {
                    await notification.SocketMessageComponent.RespondWithModalAsync(ExtensionModal.GetAutorizationModal());
                    return;
                }

                if(notification.SocketMessageComponent.Data.CustomId == $"personal_data_{notification.SocketMessageComponent.User.Id}")
                {
                    await notification.SocketMessageComponent.RespondWithModalAsync(ExtensionModal.GetPersonalInformationModal());
                    return;
                }

                await notification.SocketMessageComponent.RespondAsync($"Эта кнопочка для другого пользователя!",ephemeral: true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
