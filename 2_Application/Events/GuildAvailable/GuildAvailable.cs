using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Application.Notifications.GuildAvailable
{
    class GuildAvailable(SocketGuild socketGuild) : INotification
    {
        public SocketGuild SocketGuild { get; } = socketGuild;
    }
}
