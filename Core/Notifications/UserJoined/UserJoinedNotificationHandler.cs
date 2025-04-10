using MediatR;
using Discord_Bot.Core.Managers.RolesManagers;
using Discord_Bot.Core.Managers.UserManagers;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;

namespace Discord_Bot.Core.Notifications.UserJoined
{
    class UserJoinedNotificationHandler(
        ILogger<UserJoinedNotificationHandler> _logger,
        RolesManager _rolesManager,
        TextMessageSender _textMessageSender,
        AutorizationManager _autorizationManager) : INotificationHandler<UserJoinedNotification>
    {
        public async Task Handle(UserJoinedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.WhenAll(
                    _rolesManager.AddNotRegisteredRoleAsync(notification.SocketGuildUser),
                    _textMessageSender.SendWelcomeMessageAsync(notification.SocketGuildUser),
                    _autorizationManager.SendAutorizationCode(notification.SocketGuildUser)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
