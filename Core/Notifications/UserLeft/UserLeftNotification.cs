using Discord.WebSocket;
using MediatR;


namespace MlkAdmin.Core.Notifications.UserLeft
{
    class UserLeftNotification(SocketGuild socketGuild, SocketUser socketUser) : INotification
    {
        public SocketGuild SocketGuild { get; } = socketGuild;
        public SocketUser SocketUser { get; } = socketUser;
    }
}
