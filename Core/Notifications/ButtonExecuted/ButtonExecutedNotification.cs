using Discord.WebSocket;
using MediatR;

namespace Discord_Bot.Core.Notifications.ButtonExecuted
{
    public class ButtonExecutedNotification(SocketMessageComponent socketMessageComponent) : INotification
    {
        public SocketMessageComponent SocketMessageComponent { get; } = socketMessageComponent;
    }
}
