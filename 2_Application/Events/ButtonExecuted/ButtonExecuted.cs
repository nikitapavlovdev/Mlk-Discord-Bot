using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Application.Notifications.ButtonExecuted
{
    public class ButtonExecuted(SocketMessageComponent socketMessageComponent) : INotification
    {
        public SocketMessageComponent SocketMessageComponent { get; } = socketMessageComponent;
    }
}
