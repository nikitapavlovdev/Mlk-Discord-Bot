using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.RolesManagers;
using MlkAdmin._2_Application.Managers.EmotesManagers;
using MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.Managers.Users;

namespace MlkAdmin._2_Application.Events.GuildAvailable
{   
    class GuildAvailableHandler(
        ILogger<GuildAvailableHandler> logger,
        IDynamicMessageCenter dynamicMessageCenter,
        UserSyncService userSyncService,
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
                    voiceChannelsManager.ClearTemporaryVoiceChannels(notification.SocketGuild),
                    voiceChannelsManager.SyncVoicesAsync(notification.SocketGuild),
                    rolesManager.GuildRolesInitialization(notification.SocketGuild),
                    emotesManager.EmotesInitialization(notification.SocketGuild),
                    dynamicMessageCenter.UpdateAllDM(notification.SocketGuild.Id),
                    userSyncService.SyncUsersAsync(notification.SocketGuild)
                );
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
