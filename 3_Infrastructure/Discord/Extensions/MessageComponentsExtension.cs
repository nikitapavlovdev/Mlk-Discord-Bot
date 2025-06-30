using Discord;

namespace MlkAdmin._3_Infrastructure.Discord.Extensions
{
    public class MessageComponentsExtension
    {
        public static MessageComponent GetServerHubFeatuesButtons()
        {
            return new ComponentBuilder()
                .WithButton(new ButtonBuilder()
                    .WithStyle(ButtonStyle.Primary)
                    .WithCustomId("autolobby_naming_button")
                    .WithLabel("Моя комната"))
                .WithButton(new ButtonBuilder()
                    .WithStyle(ButtonStyle.Primary)
                    .WithCustomId($"personal_data_button")
                    .WithLabel("Обо мне"))
                .WithButton(new ButtonBuilder()
                    .WithStyle(ButtonStyle.Primary)
                    .WithCustomId("feedback_button")
                    .WithLabel("Разраб делай"))
                .WithButton(new ButtonBuilder()
                    .WithStyle(ButtonStyle.Primary)
                    .WithCustomId("any_informations_button")
                    .WithLabel("Особенности сервера"))
                .Build();
        }

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