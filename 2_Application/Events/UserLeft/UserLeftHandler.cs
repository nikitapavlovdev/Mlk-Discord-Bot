using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._2_Application.DTOs;
using MlkAdmin.Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Notifications.UserLeft
{
    class UserLeftHandler(
        ILogger<UserLeftHandler> logger,
        IModeratorLogsSender moderatorLogsSender,
        JsonChannelsMapProvider jsonChannelsMapProvider) : INotificationHandler<UserLeft>
    {
        public async Task Handle(UserLeft notification, CancellationToken cancellationToken)
        {
            try
            {
                await moderatorLogsSender.SendLogMessageAsync(new LogMessageDto()
                {
                    Description = $"Пользователь {notification.SocketUser.Mention} покинул сервер",
                    ChannelId = jsonChannelsMapProvider.RootChannel.Channels.TextChannels.AdministratorCategory.Logs.Id,
                    GuildId = notification.SocketGuild.Id,
                    Title = "User left",
                    UserId = notification.SocketGuild.Id
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
