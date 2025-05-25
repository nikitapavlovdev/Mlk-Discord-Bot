using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Managers.ChannelsManagers.TextChannelsManagers;
using MlkAdmin.Core.Managers.ChannelsManagers.VoiceChannelsManagers;
using MlkAdmin.Core.Managers.RolesManagers;
using MlkAdmin.Core.Managers.EmotesManagers;
using MlkAdmin.Core.Managers.UserManagers;

namespace MlkAdmin.Core.Notifications.GuildAvailable
{   
    class GuildAvailableNotificationHandler(
        ILogger<GuildAvailableNotificationHandler> logger,
        TextMessageManager textMessageManager,
        VoiceChannelsManager voiceChannelsManager,
        RolesManager rolesManager,
        EmotesManager emotesManager,
        StaticDataServices staticDataServices) : INotificationHandler<GuildAvailableNotification>
    {
        public async Task Handle(GuildAvailableNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.WhenAll(
                    textMessageManager.GuildTextChannelsInitialization(notification.SocketGuild),
                    voiceChannelsManager.GuildVoiceChannelsInitialization(notification.SocketGuild),
                    rolesManager.GuildRolesInitialization(notification.SocketGuild),
                    emotesManager.EmotesInitialization(notification.SocketGuild),
                    staticDataServices.LoadStaticData()
                );
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
