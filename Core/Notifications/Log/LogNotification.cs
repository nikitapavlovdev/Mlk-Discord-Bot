using Discord;
using MediatR;

namespace MlkAdmin.Core.Notifications.Log
{
    class LogNotification(LogMessage logMessage) : INotification
    {
        public LogMessage LogMessage { get; } = logMessage;
    }
}
