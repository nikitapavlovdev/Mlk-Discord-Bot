using MediatR;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Events.ButtonExecuted
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

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
