using Discord.WebSocket;
using MediatR;

namespace Discord_Bot.Core.Notifications.GuildAvailable
{
    class GuildAvailableNotification(SocketGuild socketGuild) : INotification
    {
        public SocketGuild SocketGuild { get; } = socketGuild;
    }
}
