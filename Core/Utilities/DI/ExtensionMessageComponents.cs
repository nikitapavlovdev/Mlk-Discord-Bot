using Discord;
using Microsoft.Extensions.Configuration;

namespace Discord_Bot.Core.Utilities.DI
{
    class ExtensionMessageComponents
    {
        private readonly IConfiguration _configuration;

        public ExtensionMessageComponents(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public static MessageComponent GetWelcomeMessageComponent(ulong userId)
        {
            MessageComponent messageComponent = new ComponentBuilder()
            .WithButton(new ButtonBuilder()
                .WithStyle(ButtonStyle.Primary)
                .WithCustomId($"nikname_selection_component_{userId}")
                .WithLabel("Ввести код"))
            .WithButton(new ButtonBuilder()
                .WithStyle(ButtonStyle.Primary)
                .WithCustomId($"personal_data_{userId}")
                .WithLabel("Персональные данные"))
            .Build();
                    
            return messageComponent;
        }
        public MessageComponent GetChannelRolesLinkComponent(GuildEmote guildEmote)
        {
            MessageComponent messageComponent = new ComponentBuilder()
            .WithButton(new ButtonBuilder()
                        .WithUrl(_configuration["RolesSettings:ChannelUrl"])
                        .WithLabel("Канал ролей")
                        .WithStyle(ButtonStyle.Link)
                        .WithEmote(guildEmote)).Build();

            return messageComponent;
        }
    }
}

