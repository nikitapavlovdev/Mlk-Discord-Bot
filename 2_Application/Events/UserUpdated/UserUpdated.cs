using Discord.WebSocket;
using MediatR;

namespace MlkAdmin._2_Application.Events.UserUpdated
{
    public class UserUpdated(SocketUser oldUserState, SocketUser newUserState) : INotification
    {
        public SocketUser OldUserState { get; } = oldUserState;
        public SocketUser NewUserState { get; } = newUserState;
    }
}
