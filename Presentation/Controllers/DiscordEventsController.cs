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
using Microsoft.Extensions.Logging;

namespace MlkAdmin.Presentation.Controllers
{
    public class DiscordEventsController(
        IMediator mediator,
        ILogger<DiscordEventsController> logger)
    {
        public void SubscribeOnEvents(DiscordSocketClient client)
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
            try
            {
                await mediator.Publish(new UserJoinedNotification(socketGuildUser));
            }
            catch (Exception ex)
            {
                logger.LogError("[OnUserJoined] Error - {Message}", ex.Message);
            }
        }
        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            try
            {
                await mediator.Publish(new MessageReceivedNotification(socketMessage));
            }
            catch (Exception ex)
            {
                logger.LogError("[OnMessageReceived] Error - {Message}", ex.Message);
            }

        }
        private async Task OnUserLeft(SocketGuild socketGuild, SocketUser socketUser)
        {
            try
            {
                await mediator.Publish(new UserLeftNotification(socketGuild, socketUser));
            }
            catch (Exception ex)
            {
                logger.LogError("[OnUserLeft] Error - {Message}", ex.Message);
            }
        }
        private async Task OnModalSubmitted(SocketModal socketModal)
        {
            try
            {
                await mediator.Publish(new ModalSubmittedNotification(socketModal));
            }
            catch (Exception ex)
            {
                logger.LogError("[OnModalSubmitted] Error - {Message}", ex.Message);
            }
        }
        private async Task OnButtonExecuted(SocketMessageComponent socketMessageComponent)
        {
            try
            {
                await mediator.Publish(new ButtonExecutedNotification(socketMessageComponent));

            }
            catch (Exception ex)
            {
                logger.LogError("[OnButtonExecuted] Error - {Message}", ex.Message);
            }
        }
        private async Task OnGuildAvailable(SocketGuild socketGuild)
        {
            try
            {
                await mediator.Publish(new GuildAvailableNotification(socketGuild));

            }
            catch (Exception ex)
            {
                logger.LogError("[OnGuildAvailable] Error - {Message}", ex.Message);
            }
        }
        private async Task OnUserVoiceStateUpdated(SocketUser socketUser, SocketVoiceState oldState, SocketVoiceState newState)
        {
            try
            {
                await mediator.Publish(new UserVoiceStateUpdatedNotification(socketUser, oldState, newState));
            }
            catch (Exception ex)
            {
                logger.LogError("[OnUserVoiceStateUpdated] Error - {Message}", ex.Message);

            }
        }
        private async Task OnLog(LogMessage logMessage)
        {
            try
            {
                await mediator.Publish(new LogNotification(logMessage));

            }
            catch (Exception ex)
            {
                logger.LogError("[OnLog] Error - {Message}", ex.Message);
            }
        }
        private async Task OnSelectMenuExecuted(SocketMessageComponent socketMessageComponent)
        {
            try
            {
                await mediator.Publish(new SelectMenuExecutedNotification(socketMessageComponent));
            }
            catch (Exception ex)
            {
                logger.LogError("[OnSelectMenuExecuted] Error - {Message}", ex.Message);
            }

        }
        private async Task OnReady()
        {
            try
            {
                await mediator.Publish(new ReadyNotification());
            }
            catch (Exception ex)
            {
                logger.LogError("[OnReady] Error - {Message}", ex.Message);
            }
        }
        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
        {
            try
            {
                await mediator.Publish(new ReactionAddedNotification(message, channel, reaction));
            }
            catch (Exception ex)
            {
                logger.LogError("[OnReactionAdded] Error - {Message}", ex.Message);
            }
        }
    }
}
