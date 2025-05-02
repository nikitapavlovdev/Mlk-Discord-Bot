using MediatR;
using Discord.WebSocket;

namespace Discord_Bot.Core.Notifications.MessageReceived
{
    public class MessageReceivedNotificationHandler : INotificationHandler<MessageReceivedNotification>
    {
        public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
        {
            SocketGuildUser? socketGuildUser = notification.SocketMessage.Author as SocketGuildUser;

            if(socketGuildUser.Id == 322499320735596546)
            {
                await Task.CompletedTask;
            }
        }
    }
}
