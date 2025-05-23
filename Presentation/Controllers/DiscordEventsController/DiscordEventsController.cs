using Discord;
using Discord.WebSocket;
using MediatR;
using MlkAdmin.Core.Notifications.UserJoined;
using MlkAdmin.Core.Notifications.UserLeft;
using MlkAdmin.Core.Notifications.ModalSubmitted;
using MlkAdmin.Core.Notifications.ButtonExecuted;
using MlkAdmin.Core.Notifications.GuildAvailable;
using MlkAdmin.Core.Notifications.SelectMenuExecuted;
using MlkAdmin.Core.Notifications.UserVoiceStateUpdated;
using MlkAdmin.Core.Notifications.Log;
using MlkAdmin.Core.Notifications.Ready;
using MlkAdmin.Core.Notifications.MessageReceived;
using MlkAdmin.Core.Notifications.ReactionAdded;

namespace MlkAdmin.Presentation.Controllers.DiscordEventsController
{
    class DiscordEventsController
    {
        private readonly IMediator mediator;
        public DiscordEventsController(DiscordSocketClient client, IMediator mediator)
        {
            this.mediator = mediator;   
            SubscribeOnEvents(client);
        }

        private void SubscribeOnEvents(DiscordSocketClient client)
        {
            client.Log += OnLog;
            client.UserJoined += OnUserJoined;
            client.UserLeft += OnUserLeft;
            client.ModalSubmitted += OnModalSubmitted;
            client.ButtonExecuted += OnButtonExecuted;
            client.GuildAvailable += OnGuildAvailable;
            client.UserVoiceStateUpdated += OnUserVoiceStateUpdated;
            client.SelectMenuExecuted += OnSelectMenuExecuted;
            client.Ready += OnReady;
            client.MessageReceived += OnMessageReceived;
            client.ReactionAdded += OnReactionAdded;
        }
        private async Task OnUserJoined(SocketGuildUser socketGuildUser)
        {
            await mediator.Publish(new UserJoinedNotification(socketGuildUser));
        }

        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            await mediator.Publish(new MessageReceivedNotification(socketMessage));
        }
        private async Task OnUserLeft(SocketGuild socketGuild, SocketUser socketUser)
        {
            await mediator.Publish(new UserLeftNotification(socketGuild, socketUser));
        }

        private async Task OnModalSubmitted(SocketModal socketModal)
        {
            await mediator.Publish(new ModalSubmittedNotification(socketModal));
        }

        private async Task OnButtonExecuted(SocketMessageComponent socketMessageComponent)
        {
            await mediator.Publish(new ButtonExecutedNotification(socketMessageComponent));
        }

        private async Task OnGuildAvailable(SocketGuild socketGuild)
        {
            await mediator.Publish(new GuildAvailableNotification(socketGuild));
        }

        private async Task OnUserVoiceStateUpdated(SocketUser socketUser, SocketVoiceState oldState, SocketVoiceState newState)
        {
            await mediator.Publish(new UserVoiceStateUpdatedNotification(socketUser, oldState, newState));
        }

        private async Task OnLog(LogMessage logMessage)
        {
            await mediator.Publish(new LogNotification(logMessage));
        }

        private async Task OnSelectMenuExecuted(SocketMessageComponent socketMessageComponent)
        {
            await mediator.Publish(new SelectMenuExecutedNotification(socketMessageComponent));
        }

        private async Task OnReady()
        {
            await mediator.Publish(new ReadyNotification());
        }

        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
        {
            await mediator.Publish(new ReactionAddedNotification(message, channel, reaction));
        }
    }
}
