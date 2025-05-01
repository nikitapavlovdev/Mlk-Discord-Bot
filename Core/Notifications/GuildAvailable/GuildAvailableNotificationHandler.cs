using MediatR;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;
using Discord_Bot.Core.Managers.ChannelsManagers.VoiceChannelsManagers;
using Discord_Bot.Core.Managers.RolesManagers;
using Discord_Bot.Core.Managers.EmotesManagers;

namespace Discord_Bot.Core.Notifications.GuildAvailable
{
    class GuildAvailableNotificationHandler(
        ILogger<GuildAvailableNotificationHandler> logger,
        TextMessageManager textMessageManager,
        VoiceChannelsManager voiceChannelsManager,
        RolesManager rolesManager,
        EmotesManager emotesManager) : INotificationHandler<GuildAvailableNotification>
    {
        public async Task Handle(GuildAvailableNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.WhenAll(
                    textMessageManager.GuildTextChannelsInitialization(notification.SocketGuild),
                    voiceChannelsManager.GuildVoiceChannelsInitialization(notification.SocketGuild),
                    rolesManager.GuildRolesInitialization(notification.SocketGuild),
                    emotesManager.EmotesInitialization(notification.SocketGuild)
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
