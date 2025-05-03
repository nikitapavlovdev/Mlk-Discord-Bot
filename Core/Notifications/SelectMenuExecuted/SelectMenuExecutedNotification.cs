using Discord.WebSocket;
using MediatR;

namespace Discord_Bot.Core.Notifications.SelectMenuExecuted
{
    class SelectMenuExecutedNotification(SocketMessageComponent socketMessageComponent) : INotification
    {
        public SocketMessageComponent SocketMessageComponent { get; set; } = socketMessageComponent;
    }
}
