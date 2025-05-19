using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Core.Notifications.SelectMenuExecuted
{
    class SelectMenuExecutedNotification(SocketMessageComponent socketMessageComponent) : INotification
    {
        public SocketMessageComponent SocketMessageComponent { get; set; } = socketMessageComponent;
    }
}
