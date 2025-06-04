using Discord;
using Discord.WebSocket;
using MediatR;
using MlkAdmin._2_Application.Notifications.UserJoined;
using MlkAdmin._2_Application.Notifications.UserLeft;
using MlkAdmin._2_Application.Notifications.ModalSubmitted;
using MlkAdmin._2_Application.Notifications.ButtonExecuted;
using MlkAdmin._2_Application.Notifications.GuildAvailable;
using MlkAdmin._2_Application.Notifications.SelectMenuExecuted;
using MlkAdmin._2_Application.Notifications.UserVoiceStateUpdated;
using MlkAdmin._2_Application.Notifications.Log;
using MlkAdmin._2_Application.Notifications.Ready;
using MlkAdmin._2_Application.Notifications.MessageReceived;
using MlkAdmin._2_Application.Notifications.ReactionAdded;
using Microsoft.Extensions.Logging;

namespace MlkAdmin.Presentation.DiscordListeners
{
    public class DiscordEventsListener(
        IMediator mediator,
        ILogger<DiscordEventsListener> logger)
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
                await mediator.Publish(new UserJoined(socketGuildUser));
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
                await mediator.Publish(new MessageReceived(socketMessage));
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
                await mediator.Publish(new UserLeft(socketGuild, socketUser));
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
                await mediator.Publish(new ModalSubmitted(socketModal));
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
                await mediator.Publish(new ButtonExecuted(socketMessageComponent));

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
                await mediator.Publish(new GuildAvailable(socketGuild));

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
                await mediator.Publish(new UserVoiceStateUpdated(socketUser, oldState, newState));
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
                await mediator.Publish(new Log(logMessage));

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
                await mediator.Publish(new SelectMenuExecuted(socketMessageComponent));
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
                await mediator.Publish(new Ready());
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
                await mediator.Publish(new ReactionAdded(message, channel, reaction));
            }
            catch (Exception ex)
            {
                logger.LogError("[OnReactionAdded] Error - {Message}", ex.Message);
            }
        }
    }
}
