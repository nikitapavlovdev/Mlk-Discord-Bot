using Discord;
using Microsoft.Extensions.Configuration;
using Discord_Bot.Infrastructure.Cash;
using Discord_Bot.Core.Utilities.General;

namespace Discord_Bot.Core.Utilities.DI
{
    public class ExtensionSelectionMenu(IConfiguration configuration)
    {
        private readonly IConfiguration configuration = configuration;

        public MessageComponent GetRolesSelectionMenu()
        {
            SelectMenuBuilder selectionMenuCategoryRole = new SelectMenuBuilder()
                .WithPlaceholder("Роль для категорий/каналов")
                .WithCustomId("choice_role_select")
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("📍┆Valorant player")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["Roles:Valoranter:Id"])))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("👑┆Destiny player")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["Roles:DestinyEnjoyer:Id"])))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("👨‍🎓┆IKIT Student")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["Roles:IKIT:Id"])))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("💻┆Information Hunter")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["Roles:InformationHunter:Id"])))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Очистить роли")
                    .WithValue("delete_all_roles"));

            SelectMenuBuilder selectionMenuNameColor = new SelectMenuBuilder()
                .WithPlaceholder("Цвет имени")
                .WithCustomId("choice_color_name")
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Лаймовый")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Lime:Id"])))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Хаки")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Khaki:Id"])))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Фиолетовый")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Violet:Id"])))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Малиновый")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Crimson:Id"])))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Сланцево-голубой")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Slateblue:Id"])))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Кораловый")
                    .WithValue(ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Coral:Id"])));
                

            MessageComponent component = new ComponentBuilder()
                .WithSelectMenu(selectionMenuCategoryRole)
                .WithSelectMenu(selectionMenuNameColor)
                .Build();

            return component;

        }
    }
}
