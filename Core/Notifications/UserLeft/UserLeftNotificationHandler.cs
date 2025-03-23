using Discord;
using MediatR;
using Microsoft.Extensions.Configuration;


namespace Discord_Bot.Core.Notifications.UserLeft
{
    class UserLeftNotificationHandler : INotificationHandler<UserLeftNotification>
    {
        private readonly IConfiguration configuration;

        public UserLeftNotificationHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Handle(UserLeftNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                string? startingChannelIdStr = configuration["WelcomeSettings:ChannelId"];
                if (string.IsNullOrEmpty(startingChannelIdStr))
                {
                    return;
                }

                ulong startingChannelId = ulong.Parse(startingChannelIdStr);

                Embed farewellMessage = new EmbedBuilder()
                    .WithTitle($"Прощай, {notification.SocketUser.Username}!")
                    .WithDescription($"Участник покинул сервер {notification.SocketGuild.Name}")
                    .WithColor(220, 20, 60)
                    .WithTimestamp(DateTime.UtcNow)
                    .WithFooter(new EmbedFooterBuilder()
                    .WithText(configuration["FooterSettings:FooterText"])
                    .WithIconUrl(configuration["FooterSettings:FooterIconLink"]))
                    .WithThumbnailUrl(notification.SocketUser.GetAvatarUrl(ImageFormat.Auto, 48))
                    .Build();

                ITextChannel startingChannel = notification.SocketGuild.GetTextChannel(startingChannelId);

                await Task.CompletedTask;
                //await startingChannel.SendMessageAsync(embed: farewellMessage);

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in UserLeftNotificationHandler: { ex.Message}");
            }
        }
    }
}
