using Discord.WebSocket;
using MediatR;


namespace Discord_Bot.Core.Notifications.UserLeft
{
    class UserLeftNotification : INotification
    {
        public SocketGuild SocketGuild { get; }
        public SocketUser SocketUser { get; }

        public UserLeftNotification(SocketGuild socketGuild, SocketUser socketUser)
        {
            SocketGuild = socketGuild;
            SocketUser = socketUser;
        }
    }
}
