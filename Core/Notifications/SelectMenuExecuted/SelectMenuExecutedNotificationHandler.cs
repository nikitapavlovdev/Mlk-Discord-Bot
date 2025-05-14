using MediatR;
using Discord_Bot.Core.Utilities.General;
using Discord_Bot.Core.Managers.RolesManagers;

namespace Discord_Bot.Core.Notifications.SelectMenuExecuted
{
    class SelectMenuExecutedNotificationHandler(
        RolesManager rolesManager) : INotificationHandler<SelectMenuExecutedNotification>
    {
        public async Task Handle (SelectMenuExecutedNotification notification, CancellationToken cancellationToken)
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
                        await rolesManager.SetColorNameRole(notification.SocketMessageComponent.User, ExtensionStaticMethods.ConvertId(notification.SocketMessageComponent.Data.Values.ElementAt(0)));

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
