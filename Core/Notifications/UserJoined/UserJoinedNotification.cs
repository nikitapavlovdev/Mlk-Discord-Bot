using Discord.WebSocket;
using MediatR;

namespace Discord_Bot.Core.Notifications.UserJoined
{
    class UserJoinedNotification(SocketGuildUser socketGuildUser) : INotification
    {
        public SocketGuildUser SocketGuildUser { get; } = socketGuildUser;
    }
}
