using Discord;
using Discord.WebSocket;
using MediatR;

namespace MlkAdmin.Core.Notifications.ReactionAdded
{
    public class ReactionAddedNotification(
        Cacheable<IUserMessage, ulong> message, 
        Cacheable<IMessageChannel, ulong> channel, 
        SocketReaction reaction) : INotification
    {
        public Cacheable<IUserMessage, ulong> Message { get; set; } = message;
        public Cacheable<IMessageChannel, ulong> Channel { get; set; } = channel;
        public SocketReaction Reaction { get; set; } = reaction;
    }
}
