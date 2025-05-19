using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Core.Notifications.MessageReceived
{
    public class MessageReceivedNotification(SocketMessage socketMessage) : INotification
    {
        public SocketMessage SocketMessage { get; set; } = socketMessage;
    }
}
