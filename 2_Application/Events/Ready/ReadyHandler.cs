using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Events.Ready
{
    public class ReadyHandler(
        ILogger<ReadyHandler> logger) : INotificationHandler<Ready>
    {
        public async Task Handle(Ready notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
