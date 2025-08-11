using System.Text;
using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using MlkAdmin.Core.Utilities.General;

namespace MlkAdmin._2_Application.Events.UserUpdated
{
    public class GuildMemberUpdatedHandler(
        IModeratorLogsSender moderatorLogsSender,
        ILogger<GuildMemberUpdatedHandler> logger,
        JsonDiscordChannelsMapProvider jsonDiscordChannelsMapProvider,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider) : INotificationHandler<GuildMemberUpdated>
    {
        public async Task Handle(GuildMemberUpdated notification, CancellationToken cancellationToken)
        {
			try
			{
                StringBuilder descriptionBuilder = new();

                descriptionBuilder.CompareChange("Имя пользователя", notification.OldUserState.Value.Username, notification.NewUserState.Username);
                descriptionBuilder.CompareChange("Глобальное имя", notification.OldUserState.Value.GlobalName, notification.NewUserState.GlobalName);
                descriptionBuilder.CompareChange("Никнейм", notification.OldUserState.Value.Nickname, notification.NewUserState.Nickname);

                if(descriptionBuilder.Length == 0)
                {
                    return;
                }

                await moderatorLogsSender.SendLogMessageAsync(new DTOs.LogMessageDto
                {
                    ChannelId = jsonDiscordChannelsMapProvider.LogsChannelId,
                    Title = $"Изменение участника {notification.OldUserState.Value.GlobalName ?? "-"}",
                    Description = descriptionBuilder.ToString(),
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
