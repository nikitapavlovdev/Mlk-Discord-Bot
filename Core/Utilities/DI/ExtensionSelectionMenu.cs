using Discord;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Core.Utilities.DI
{
    public class ExtensionSelectionMenu(JsonDiscordRolesProvider jsonDiscordRolesProvider)
    {
        public MessageComponent GetColorSwitchSelectionMenu()
        {
            SelectMenuBuilder selectionMenuNameColor = new SelectMenuBuilder()
                .WithPlaceholder("жʍяᴋни")
                .WithCustomId("choice_color_name")
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("💜")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Violet.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("💙")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Slateblue.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("🧡")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Coral.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("💛")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Khaki.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("💖")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Crimson.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("💚")
                    .WithValue(jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Lime.Id.ToString()))
                .AddOption(new SelectMenuOptionBuilder()
                    .WithLabel("ʀᴇᴍᴏᴠᴇ ᴄᴏʟᴏʀ")
                    .WithValue("remove_color"));
                

            MessageComponent component = new ComponentBuilder()
                .WithSelectMenu(selectionMenuNameColor)
                .Build();

            return component;

        }
    }
}
