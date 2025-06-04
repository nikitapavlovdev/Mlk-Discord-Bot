using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Application.Notifications.SelectMenuExecuted
{
    class SelectMenuExecuted(SocketMessageComponent socketMessageComponent) : INotification
    {
        public SocketMessageComponent SocketMessageComponent { get; set; } = socketMessageComponent;
    }
}
