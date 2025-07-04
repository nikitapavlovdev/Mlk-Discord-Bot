using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Events.UserUpdated
{
    public class UserUpdatedHandler(IModeratorLogsSender moderatorLogsSender, 
        ILogger<UserUpdatedHandler> logger,
        JsonDiscordChannelsMapProvider jsonDiscordChannelsMapProvider,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider) : INotificationHandler<UserUpdated>
    {
        public async Task Handle(UserUpdated notification, CancellationToken cancellationToken)
        {
			try
			{
                string title = "Пользователь изменен";
                string decription = $"> **Глобальное имя**: {notification.OldUserState.GlobalName} → {notification.NewUserState.GlobalName}\n" +
                    $"> **Имя пользователя**: {notification.OldUserState.Username} → {notification.NewUserState.Username}\n" +
                    $"> **Статус**: {notification.OldUserState.Status} → {notification.NewUserState.Status}";

                await moderatorLogsSender.SendLogMessageAsync(new DTOs.LogMessageDto
                {
                    ChannelId = jsonDiscordChannelsMapProvider.LogsChannelId,
                    Title = title,
                    Description = decription,
                    UserId = notification.NewUserState.Id,
                    GuildId = jsonDiscordConfigurationProvider.GuildId
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
