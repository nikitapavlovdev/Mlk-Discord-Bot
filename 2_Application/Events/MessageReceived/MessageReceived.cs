using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Application.Notifications.MessageReceived
{
    public class MessageReceived(SocketMessage socketMessage) : INotification
    {
        public SocketMessage SocketMessage { get; set; } = socketMessage;
    }
}
