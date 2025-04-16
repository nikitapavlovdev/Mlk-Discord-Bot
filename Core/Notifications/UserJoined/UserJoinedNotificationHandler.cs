using MediatR;
using Discord_Bot.Core.Managers.RolesManagers;
using Discord_Bot.Core.Managers.UserManagers;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;

namespace Discord_Bot.Core.Notifications.UserJoined
{
    class UserJoinedNotificationHandler(
        ILogger<UserJoinedNotificationHandler> logger,
        RolesManager rolesManager,
        TextMessageManager textMessageSender,
        AutorizationManager autorizationManager) : INotificationHandler<UserJoinedNotification>
    {
        public async Task Handle(UserJoinedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.WhenAll(
                    rolesManager.AddNotRegisteredRoleAsync(notification.SocketGuildUser),
                    textMessageSender.SendWelcomeMessageAsync(notification.SocketGuildUser),
                    autorizationManager.SendAutorizationCode(notification.SocketGuildUser)
                );
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
