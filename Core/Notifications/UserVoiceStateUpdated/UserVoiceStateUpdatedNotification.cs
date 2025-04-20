using MediatR;
using Discord.WebSocket;

namespace Discord_Bot.Core.Notifications.UserVoiceStateUpdated
{
    class UserVoiceStateUpdatedNotification(SocketUser socketUser, SocketVoiceState oldState, SocketVoiceState newState) : INotification
    {
        public SocketUser SocketUser { get; } = socketUser;
        public SocketVoiceState OldState { get; } = oldState;
        public SocketVoiceState NewState { get; } = newState;
    }
}
