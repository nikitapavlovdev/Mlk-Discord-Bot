using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Core.Notifications.UserJoined
{
    class UserJoinedNotification(SocketGuildUser socketGuildUser) : INotification
    {
        public SocketGuildUser SocketGuildUser { get; } = socketGuildUser;
    }
}
