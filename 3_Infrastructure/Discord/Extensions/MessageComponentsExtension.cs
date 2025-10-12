using Discord;

namespace MlkAdmin._3_Infrastructure.Discord.Extensions
{
    public class MessageComponentsExtension
    {
        public static MessageComponent GetServerHubLinkButton(string serverhubChannelLink)
        {
            return new ComponentBuilder()
                .WithButton(new ButtonBuilder()
                    .WithStyle(ButtonStyle.Link)
                    .WithLabel("Пройти верификацию")
                    .WithUrl(serverhubChannelLink))
                .Build();
        }
    }
}