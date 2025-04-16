using Discord.WebSocket;
using MediatR;
using Discord_Bot.Core.Utilities.General;
using Discord_Bot.Infrastructure.Cache;
using Discord_Bot.Core.Managers.RolesManagers;

namespace Discord_Bot.Core.Notifications.SelectMenuExecuted
{
    class SelectMenuExecutedNotificationHandler(
        RolesCache rolesCache,
        RolesManager rolesManager) : INotificationHandler<SelectMenuExecutedNotification>
    {
        public async Task Handle (SelectMenuExecutedNotification notification, CancellationToken cancellationToken)
        {
			try
			{
                await notification.SocketMessageComponent.DeferAsync();

                if (notification.SocketMessageComponent.User is not SocketGuildUser socketGuildUser)
                {
                    return;
                }

                IReadOnlyCollection<string> values = notification.SocketMessageComponent.Data.Values;

                if (notification.SocketMessageComponent.Data.CustomId == "choice_role_select")
                {
                    string value = values.ElementAt(0);

                    if (value == "delete_all_roles")
                    {
                        await rolesManager.DeleteSelectionRoles(notification.SocketMessageComponent.User as SocketGuildUser);
                        return;
                    }

                    if (!socketGuildUser.Roles.Any(role => role.Id == ExtensionMethods.ConvertId(values.ElementAt(0))))
                    {
                        await socketGuildUser.AddRoleAsync(rolesCache.GetRole(ExtensionMethods.ConvertId(values.ElementAt(0))));
                    }
                }
                if (notification.SocketMessageComponent.Data.CustomId == "choice_color_name")
                {
                    Dictionary<ulong, SocketRole> ColorNameList = rolesCache.GetColorNameDictionaryForCheck();

                    foreach(SocketRole roleInCycle in ColorNameList.Values)
                    {
                        if(socketGuildUser.Roles.Any(role => role.Id == roleInCycle.Id))
                        {
                            await socketGuildUser.RemoveRoleAsync(roleInCycle.Id);
                        }
                    }

                    foreach(var role in notification.SocketMessageComponent.Data.Values)
                    {
                        await socketGuildUser.AddRoleAsync(rolesCache.GetRole(ExtensionMethods.ConvertId(role)));
                        break;
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
