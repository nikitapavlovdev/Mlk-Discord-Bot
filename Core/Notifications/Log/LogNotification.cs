using Discord;
using MediatR;

namespace Discord_Bot.Core.Notifications.Log
{
    class LogNotification(LogMessage logMessage) : INotification
    {
        public LogMessage LogMessage { get; } = logMessage;
    }
}
