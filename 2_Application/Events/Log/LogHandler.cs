using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Events.Log
{
    class LogHandler(ILogger<LogHandler> logger) : INotificationHandler<Log>
    {
        public async Task Handle(Log notification, CancellationToken cancellationToken)
        {
			try
			{
                logger.LogInformation("Лог-сообщение: {Message}", notification.LogMessage.Message);

                await Task.CompletedTask;
            }
			catch (Exception ex)
			{
                logger.LogError("Error: {ExMessage}", ex.Message);
			}
        }
    }
}
