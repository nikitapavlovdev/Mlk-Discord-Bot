using Discord.WebSocket;
using MediatR;

namespace MlkAdmin._2_Application.Notifications.SelectMenuExecuted
{
    class SelectMenuExecuted(SocketMessageComponent socketMessageComponent) : INotification
    {
        public SocketMessageComponent SocketMessageComponent { get; set; } = socketMessageComponent;
    }
}
