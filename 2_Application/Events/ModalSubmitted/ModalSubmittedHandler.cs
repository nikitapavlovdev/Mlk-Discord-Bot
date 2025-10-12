using MediatR;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Events.ModalSubmitted
{
    class ModalSubmittedHandler(
        ILogger<ModalSubmittedHandler> logger) : INotificationHandler<ModalSubmitted>
    {
        public async Task Handle(ModalSubmitted notification, CancellationToken cancellationToken)
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
                    default:
                        logger.LogInformation("Неизвестный CustomId: {CustomId}", notification.Modal.Data.CustomId);
                        break;

                }

                await Task.CompletedTask;
            }
            catch (Exception ex) 
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
