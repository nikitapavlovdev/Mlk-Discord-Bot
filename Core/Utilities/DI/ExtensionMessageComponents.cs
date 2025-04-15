using Discord;
using Discord_Bot.Core.Providers.JsonProvider;
using Microsoft.Extensions.Configuration;

namespace Discord_Bot.Core.Utilities.DI
{
    class ExtensionMessageComponents
    {
        public static MessageComponent GetWelcomeMessageComponent(ulong userId)
        {
            return new ComponentBuilder()
            .WithButton(new ButtonBuilder()
                .WithStyle(ButtonStyle.Primary)
                .WithCustomId($"nikname_selection_component_{userId}")
                .WithLabel("Ввести код"))
            .WithButton(new ButtonBuilder()
                .WithStyle(ButtonStyle.Primary)
                .WithCustomId($"personal_data_{userId}")
                .WithLabel("Персональные данные"))
            .Build();
        }
    }
}

