using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.RolesManagers;
using MlkAdmin._2_Application.Managers.EmotesManagers;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannels;
using MlkAdmin._1_Domain.Interfaces.Users;
using MlkAdmin._3_Infrastructure.Cache.Channels;
using MlkAdmin._3_Infrastructure.Cache.Users;

namespace MlkAdmin._2_Application.Events.GuildAvailable
{   
    class GuildAvailableHandler(
        ILogger<GuildAvailableHandler> logger,
        IDynamicMessageCenter dynamicMessageCenter,
        IUserSyncService userSyncService,
        VoiceChannelsManager voiceChannelsManager,
        VoiceChannelSyncServices voiceChannelSyncServices,
        RolesManager rolesManager,
        EmotesManager emotesManager,
        ChannelsCache channelsCache,
        UsersCache usersCache) : INotificationHandler<GuildAvailable>
    {
        public async Task Handle(GuildAvailable notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.WhenAll(
                    voiceChannelsManager.UpsertGuildVoiceChannelsAsync(notification.SocketGuild),
                    rolesManager.GuildRolesInitialization(notification.SocketGuild),
                    emotesManager.EmotesInitialization(notification.SocketGuild),
                    dynamicMessageCenter.UpdateAllDM(notification.SocketGuild.Id),
                    userSyncService.SyncUsersAsync(notification.SocketGuild.Id),
                    voiceChannelSyncServices.SyncVoiceChannelsDbWithGuildAsync(notification.SocketGuild),
                    channelsCache.FillChannelsAsync(notification.SocketGuild.Channels),
                    usersCache.FillUsers(notification.SocketGuild)
                );
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
