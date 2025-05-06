using Discord;

namespace Discord_Bot.Core.Utilities.DI
{
    class ExtensionMessageComponents
    {
        public static MessageComponent GetWelcomeMessageComponent(ulong userId)
        {
            return new ComponentBuilder()
            .WithButton(new ButtonBuilder()
                .WithStyle(ButtonStyle.Primary)
                .WithCustomId($"au_{userId}")
                .WithLabel("Ввести код"))
            .Build();
        }

        public static MessageComponent GetAdditionalWelcomeMessageComponent(ulong userId)
        {
            return new ComponentBuilder()
            .WithButton(new ButtonBuilder()
                .WithStyle(ButtonStyle.Primary)
                .WithCustomId($"personal_data_{userId}")
                .WithLabel("Персональные данные"))
            .Build();
        }
    }
}