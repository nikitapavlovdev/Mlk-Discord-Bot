using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin.Core.Notifications.Log
{
    class LogNotificationHandler(ILogger<LogNotificationHandler> _logger) : INotificationHandler<LogNotification>
    {
        public async Task Handle(LogNotification notification, CancellationToken cancellationToken)
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
