using Discord;

namespace MlkAdmin.Core.Utilities.DI
{
    class ExtensionMessageComponents
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
                .Build();
        }
    }
}