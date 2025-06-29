using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.UserManagers;
using MlkAdmin._2_Application.Managers.RolesManagers;
using MlkAdmin._2_Application.Managers.EmotesManagers;
using MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers;

namespace MlkAdmin._2_Application.Notifications.GuildAvailable
{   
    class GuildAvailableHandler(
        ILogger<GuildAvailableHandler> logger,
        TextMessageManager textMessageManager,
        VoiceChannelsManager voiceChannelsManager,
        RolesManager rolesManager,
        EmotesManager emotesManager) : INotificationHandler<GuildAvailable>
    {
        public async Task Handle(GuildAvailable notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.WhenAll(
                    textMessageManager.GuildTextChannelsInitialization(notification.SocketGuild),
                    voiceChannelsManager.GuildVoiceChannelsInitialization(notification.SocketGuild),
                    rolesManager.GuildRolesInitialization(notification.SocketGuild),
                    emotesManager.EmotesInitialization(notification.SocketGuild)
                );
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
