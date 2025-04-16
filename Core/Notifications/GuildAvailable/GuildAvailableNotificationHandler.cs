using MediatR;
using Discord_Bot.Infrastructure.Cache;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;
using Discord_Bot.Core.Managers.ChannelsManagers.VoiceChannelsManagers;
using Discord_Bot.Core.Managers.RolesManagers;

namespace Discord_Bot.Core.Notifications.GuildAvailable
{
    class GuildAvailableNotificationHandler(
        EmotesCache emotesCache,
        ILogger<GuildAvailableNotificationHandler> logger,
        TextMessageManager textMessageManager,
        VoiceChannelsManager voiceChannelsManager,
        RolesManager rolesManager) : INotificationHandler<GuildAvailableNotification>
    {
        public async Task Handle(GuildAvailableNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.WhenAll(
                    textMessageManager.GuildTextChannelsInitialization(notification.SocketGuild),
                    voiceChannelsManager.GuildVoiceChannelsInitialization(notification.SocketGuild),
                    rolesManager.GuildRolesInitialization(notification.SocketGuild),
                    emotesCache.EmotesInitialization(notification.SocketGuild),
                    textMessageManager.SendMessageWithGuildRoles(notification.SocketGuild)
                );

                logger.LogInformation("Guild entities has been loaded");

            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
