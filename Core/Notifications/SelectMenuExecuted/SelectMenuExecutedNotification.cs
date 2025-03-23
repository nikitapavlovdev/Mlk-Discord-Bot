using Discord.WebSocket;
using MediatR;


namespace Discord_Bot.Core.Notifications.SelectMenuExecuted
{
    class SelectMenuExecutedNotification : INotification
    {
        public SocketMessageComponent SocketMessageComponent { get; set; }

        public SelectMenuExecutedNotification(SocketMessageComponent socketMessageComponent)
        {
            this.SocketMessageComponent = socketMessageComponent;
        }
    }
}
