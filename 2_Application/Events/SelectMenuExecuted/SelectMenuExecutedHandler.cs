using MediatR;
using MlkAdmin.Core.Utilities.General;
using MlkAdmin._2_Application.Managers.RolesManagers;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Notifications.SelectMenuExecuted
{
    class SelectMenuExecutedHandler(
        RolesManager rolesManager,
        ILogger<SelectMenuExecutedHandler> logger) : INotificationHandler<SelectMenuExecuted>
    {
        public async Task Handle (SelectMenuExecuted notification, CancellationToken cancellationToken)
        {
			try
			{
                await notification.SocketMessageComponent.DeferAsync();

                switch (notification.SocketMessageComponent.Data.CustomId)
                {
                    case "choice_color_name":
                        await rolesManager.RemoveHavingSwitchColorRole(notification.SocketMessageComponent.User);

                        if (notification.SocketMessageComponent.Data.Values.ElementAt(0) != "remove_color")
                        {
                            await rolesManager.SetColorNameRole(notification.SocketMessageComponent.User, notification.SocketMessageComponent.Data.Values.ElementAt(0).ConvertId());
                        }
                        break;

                    default:
                        break;
                }
			}
			catch (Exception ex)
			{
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
