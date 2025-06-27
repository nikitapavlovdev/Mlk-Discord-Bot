using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Notifications.Log
{
    class LogHandler(ILogger<LogHandler> _logger) : INotificationHandler<Log>
    {
        public async Task Handle(Log notification, CancellationToken cancellationToken)
        {
			try
			{
                _logger.LogInformation("Лог-сообщение: {Message}", notification.LogMessage.Message);

                await Task.CompletedTask;
            }
			catch (Exception ex)
			{
                _logger.LogError("Error: {ExMessage}", ex.Message);
			}
        }
    }
}
