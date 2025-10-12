using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.RolesManagers;
using MlkAdmin._2_Application.DTOs.Discord.Messages;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using MlkAdmin._3_Infrastructure.Cache.Users;
using MlkAdmin._2_Application.Services.Messages;

namespace MlkAdmin._2_Application.Events.UserJoined
{
    class UserJoinedHandler(
        ILogger<UserJoinedHandler> logger,
        IModeratorLogsSender moderatorLogsSender,
        WelcomeService welcomeService,
        UsersCache usersCache,
        RolesManager rolesManager,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider) : INotificationHandler<UserJoined>
    {
        public async Task Handle(UserJoined notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.SocketGuildUser.IsBot) { return; }

                await rolesManager.AddNotRegisteredRoleAsync(notification.SocketGuildUser);
                await welcomeService.SendWelcomeMessageAsync(notification.SocketGuildUser);
                await usersCache.AddUserAsync(notification.SocketGuildUser);

                await moderatorLogsSender.SendLogMessageAsync(new LogMessageDto
                {
                    Description = $"> Пользователь {notification.SocketGuildUser.Mention} присоединился к серверу",
                    Title = "Новый пользователь",
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