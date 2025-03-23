using MediatR;
using Discord;
using Discord.WebSocket;

namespace Discord_Bot.Core.Notifications.ModalSubmitted
{
    class ModalSubmittedNotification(SocketModal modal) : INotification
    {
        public SocketModal Modal { get; } = modal;
    }
}
