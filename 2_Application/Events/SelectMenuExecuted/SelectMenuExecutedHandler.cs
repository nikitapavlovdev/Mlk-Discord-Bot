using MediatR;
using MlkAdmin.Core.Utilities.General;
using MlkAdmin._2_Application.Managers.RolesManagers;

namespace MlkAdmin._2_Application.Notifications.SelectMenuExecuted
{
    class SelectMenuExecutedHandler(
        RolesManager rolesManager) : INotificationHandler<SelectMenuExecuted>
    {
        public async Task Handle (SelectMenuExecuted notification, CancellationToken cancellationToken)
        {
			try
			{
                await notification.SocketMessageComponent.DeferAsync();

                if (notification.SocketMessageComponent.Data.CustomId == "choice_role_select")
                {
                    
                }
                if (notification.SocketMessageComponent.Data.CustomId == "choice_color_name")
                {
                    await rolesManager.RemoveHavingSwitchColorRole(notification.SocketMessageComponent.User);

                    if (notification.SocketMessageComponent.Data.Values.ElementAt(0) != "remove_color")
                    {
                        await rolesManager.SetColorNameRole(notification.SocketMessageComponent.User, notification.SocketMessageComponent.Data.Values.ElementAt(0).ConvertId());

                    }
                }
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
			}
        }
    }
}
