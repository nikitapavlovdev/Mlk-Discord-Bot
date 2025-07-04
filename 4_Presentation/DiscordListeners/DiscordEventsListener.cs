using Discord;
using Discord.WebSocket;
using MediatR;
using MlkAdmin._2_Application.Events.UserJoined;
using MlkAdmin._2_Application.Events.UserLeft;
using MlkAdmin._2_Application.Events.ModalSubmitted;
using MlkAdmin._2_Application.Events.ButtonExecuted;
using MlkAdmin._2_Application.Events.GuildAvailable;
using MlkAdmin._2_Application.Events.SelectMenuExecuted;
using MlkAdmin._2_Application.Events.UserVoiceStateUpdated;
using MlkAdmin._2_Application.Events.Log;
using MlkAdmin._2_Application.Events.Ready;
using MlkAdmin._2_Application.Events.MessageReceived;
using MlkAdmin._2_Application.Events.ReactionAdded;
using MlkAdmin._2_Application.Events.UserUpdated;
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
            client.UserUpdated += OnUserUpdated;
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
        private async Task OnUserUpdated(SocketUser oldUserState, SocketUser newUserState)
        {
            try
            {
                await mediator.Publish(new UserUpdated(oldUserState, newUserState));
            }
            catch (Exception ex)
            {
                logger.LogError("[OnUserUpdated] Error - {Message}", ex.Message);

            }
        }
    }
}
