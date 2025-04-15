using Discord;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Core.Utilities.DI
{
    public class ExtensionSelectionMenu(JsonDiscordRolesProvider jsonDiscordRolesProvider)
    {
        public MessageComponent GetRolesSelectionMenu()
        {
            SelectMenuBuilder selectionMenuCategoryRole = new SelectMenuBuilder()
                .WithPlaceholder("Роль для категорий/каналов")
                .WithCustomId("choice_role_select")
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("📍┆Valorant player")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Valoranter.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("👑┆Destiny player")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.DestinyEnjoyer.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("👨‍🎓┆IKIT Student")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("💻┆Information Hunter")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Очистить роли")
                    .WithValue("delete_all_roles"));

            SelectMenuBuilder selectionMenuNameColor = new SelectMenuBuilder()
                .WithPlaceholder("Цвет имени")
                .WithCustomId("choice_color_name")
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Лаймовый")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Lime.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Хаки")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Khaki.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Фиолетовый")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Violet.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Малиновый")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Crimson.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Сланцево-голубой")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Slateblue.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("Кораловый")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Coral.Id.ToString()));
                

            MessageComponent component = new ComponentBuilder()
                .WithSelectMenu(selectionMenuCategoryRole)
                .WithSelectMenu(selectionMenuNameColor)
                .Build();

            return component;

        }
    }
}
