using Discord;
using Discord.WebSocket;
using MediatR;
using Discord_Bot.Core.Notifications.UserJoined;
using Discord_Bot.Core.Notifications.UserLeft;
using Discord_Bot.Core.Notifications.ModalSubmitted;
using Discord_Bot.Core.Notifications.ButtonExecuted;
using Discord_Bot.Core.Notifications.GuildAvailable;
using Discord_Bot.Core.Notifications.SelectMenuExecuted;
using Discord_Bot.Core.Notifications.UserVoiceStateUpdated;
using Discord_Bot.Core.Notifications.Log;

namespace Discord_Bot.Presentation.Controllers.DiscordEventsController
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
        }
        private async Task OnUserJoined(SocketGuildUser socketGuildUser)
        {
            await mediator.Publish(new UserJoinedNotification(socketGuildUser));
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
    }
}
