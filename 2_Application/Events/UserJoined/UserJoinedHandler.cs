using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.RolesManagers;
using MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Events.UserJoined
{
    class UserJoinedHandler(
        ILogger<UserJoinedHandler> logger,
        IModeratorLogsSender moderatorLogsSender,
        RolesManager rolesManager,
        TextMessageManager textMessageManager,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider) : INotificationHandler<UserJoined>
    {
        public async Task Handle(UserJoined notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.SocketGuildUser.IsBot) { return; }

                await rolesManager.AddNotRegisteredRoleAsync(notification.SocketGuildUser);
                await textMessageManager.SendWelcomeMessageAsync(notification.SocketGuildUser);
                await moderatorLogsSender.SendLogMessageAsync(new DTOs.LogMessageDto
                {
                    Description = $"> Пользователь {notification.SocketGuildUser.Mention} присоединился к серверу",
                    Title = "Новый пользователь",
                    GuildId = notification.SocketGuildUser.Guild.Id,
                    ChannelId = jsonChannelsMapProvider.LogsChannelId,
                    UserId = notification.SocketGuildUser.Id

                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при работе события UserJoinedHandler");
            }
        }
    }
}