using Discord;
using MediatR;
using Microsoft.Extensions.Configuration;


namespace Discord_Bot.Core.Notifications.UserLeft
{
    class UserLeftNotificationHandler() : INotificationHandler<UserLeftNotification>
    {
        public async Task Handle(UserLeftNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in UserLeftNotificationHandler: { ex.Message}");
            }
        }
    }
}
