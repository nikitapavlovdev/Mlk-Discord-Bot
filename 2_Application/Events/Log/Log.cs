using Discord;
using MediatR;

namespace MlkAdmin._2_Application.Events.Log
{
    class Log(LogMessage logMessage) : INotification
    {
        public LogMessage LogMessage { get; } = logMessage;
    }
}
