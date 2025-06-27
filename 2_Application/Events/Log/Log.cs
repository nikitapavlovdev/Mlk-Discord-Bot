using Discord;
using MediatR;

namespace MlkAdmin._2_Application.Notifications.Log
{
    class Log(LogMessage logMessage) : INotification
    {
        public LogMessage LogMessage { get; } = logMessage;
    }
}
