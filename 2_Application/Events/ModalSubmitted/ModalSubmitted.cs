using MediatR;
using Discord.WebSocket;

namespace MlkAdmin.Application.Notifications.ModalSubmitted
{
    class ModalSubmitted(SocketModal modal) : INotification
    {
        public SocketModal Modal { get; } = modal;
    }
}
