using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Application.Notifications.UserJoined
{
    class UserJoined(SocketGuildUser socketGuildUser) : INotification
    {
        public SocketGuildUser SocketGuildUser { get; } = socketGuildUser;
    }
}
