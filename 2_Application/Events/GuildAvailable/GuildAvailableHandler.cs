using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin.Application.Managers.UserManagers;
using MlkAdmin.Application.Managers.RolesManagers;
using MlkAdmin.Application.Managers.EmotesManagers;
using MlkAdmin.Application.Managers.ChannelsManagers.TextChannelsManagers;
using MlkAdmin.Application.Managers.ChannelsManagers.VoiceChannelsManagers;

namespace MlkAdmin.Application.Notifications.GuildAvailable
{   
    class GuildAvailableHandler(
        ILogger<GuildAvailableHandler> logger,
        TextMessageManager textMessageManager,
        VoiceChannelsManager voiceChannelsManager,
        RolesManager rolesManager,
        EmotesManager emotesManager,
        StaticDataServices staticDataServices) : INotificationHandler<GuildAvailable>
    {
        public async Task Handle(GuildAvailable notification, CancellationToken cancellationToken)
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
