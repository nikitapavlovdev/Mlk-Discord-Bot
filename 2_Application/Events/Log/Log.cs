using Discord;
using MediatR;

namespace MlkAdmin.Application.Notifications.Log
{
    class Log(LogMessage logMessage) : INotification
    {
        public LogMessage LogMessage { get; } = logMessage;
    }
}
