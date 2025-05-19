using MediatR;
using Discord.WebSocket;

namespace MlkAdmin.Core.Notifications.ModalSubmitted
{
    class ModalSubmittedNotification(SocketModal modal) : INotification
    {
        public SocketModal Modal { get; } = modal;
    }
}
