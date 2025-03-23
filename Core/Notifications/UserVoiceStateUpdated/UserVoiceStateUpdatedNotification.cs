using MediatR;
using Discord.WebSocket;

namespace Discord_Bot.Core.Notifications.UserVoiceStateUpdated
{
    class UserVoiceStateUpdatedNotification : INotification
    {
        public SocketUser SocketUser { get; }
        public SocketVoiceState OldState { get; }
        public SocketVoiceState NewState { get; }

        public UserVoiceStateUpdatedNotification(SocketUser socketUser, SocketVoiceState oldState, SocketVoiceState newState)
        {
            SocketUser = socketUser;
            OldState = oldState;
            NewState = newState;
        }
    }
}
