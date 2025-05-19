using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Core.Notifications.ButtonExecuted
{
    public class ButtonExecutedNotification(SocketMessageComponent socketMessageComponent) : INotification
    {
        public SocketMessageComponent SocketMessageComponent { get; } = socketMessageComponent;
    }
}
