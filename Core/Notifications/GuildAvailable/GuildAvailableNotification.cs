using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Core.Notifications.GuildAvailable
{
    class GuildAvailableNotification(SocketGuild socketGuild) : INotification
    {
        public SocketGuild SocketGuild { get; } = socketGuild;
    }
}
