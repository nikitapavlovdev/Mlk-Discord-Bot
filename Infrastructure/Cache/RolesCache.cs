using Discord.WebSocket;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Infrastructure.Cache
{
    public class RolesCache(
        EmotesCache emotesCash, 
        JsonDiscordRolesProvider jsonDiscordRolesProvider,
        JsonDiscordEmotesProvider jsonDiscordEmotesProvider)
    {
        private readonly Dictionary<ulong, SocketRole> BaseRoles = [];
        private readonly Dictionary<ulong, SocketRole> GamingRoles = [];
        private readonly Dictionary<ulong, SocketRole> UniqieRoles = [];
        private readonly Dictionary<ulong, SocketRole> AvailableForSelectionRoles = [];
        private readonly Dictionary<ulong, SocketRole> GuildRoles = [];
        private readonly Dictionary<ulong, SocketRole> SwitchColorRoles = [];

        private bool IsSwitchColorRole(SocketRole role)
        {
            if (role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Coral.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Khaki.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Violet.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Crimson.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Lime.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Slateblue.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsBaseServerRole(SocketRole role)
        {
            if (role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.Moderator.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsGamingRole(SocketRole role)
        {
            if (role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.DestinyEnjoyer.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Valoranter.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsUniqueRole(SocketRole role)
        {
            if (role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.DeadInside.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.International.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsAvailableForSelectionRole(SocketRole role)
        {
            if (role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.DestinyEnjoyer.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Valoranter.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddRole(SocketRole role)
        {
            GuildRoles.TryAdd(role.Id, role);

            if (IsBaseServerRole(role))
            {
                BaseRoles.TryAdd(role.Id, role);
            }

            if (IsGamingRole(role))
            {
                GamingRoles.TryAdd(role.Id, role);
            }

            if (IsSwitchColorRole(role))
            {
                SwitchColorRoles.TryAdd(role.Id, role);
            }

            if (IsUniqueRole(role))
            {
                UniqieRoles.TryAdd(role.Id, role);
            }

            if (IsAvailableForSelectionRole(role))
            {
                AvailableForSelectionRoles.TryAdd(role.Id, role);
            }
        }

        public string GetDescriptionForСhoiceRoles()
        {
            string description = $"Доступные для выбора роли. " +
                $"\nВ выпадающем списке просто выбери то, что тебе интересно {emotesCash.GetEmote(jsonDiscordEmotesProvider.RootDiscordEmotes.StaticEmotes.StaticZero.Love.Id)}\n" +
                $"### Открывающие категории/каналы\n\n";

            foreach (var role in AvailableForSelectionRoles)
            {
                description += $"> {role.Value.Mention}\n";
            }

            description += "### Изменение цвета имени\n\n";

            foreach (var role in SwitchColorRoles)
            {
                description += $"> {role.Value.Mention}\n";
            }

            return description;
        }
        public string GetDescriptionInfoAboutRoles()
        {
            string description = "В данном блоке находится общая информация об основных ролях нашего сервера, " +
                "а так же блок с ролями, которые можно получить прямо сейчас\n";

            description += "## Главное\n\n";

            foreach (var role in BaseRoles)
            {
                description += $"> {role.Value.Mention}\n";
            }

            description += "## Игры\n\n";

            foreach (var role in GamingRoles)
            {
                description += $"> {role.Value.Mention}\n";
            }

            description += "## Прочее\n\n";

            foreach (var role in UniqieRoles)
            {
                description += $"> {role.Value.Mention}\n";
            }

            return description;
        }

        public SocketRole GetRole(ulong roleId)
        {
            SocketRole role = GuildRoles[roleId];

            if (role != null)
            {
                return role;
            }

            throw new KeyNotFoundException($"Role with ID {roleId} not found.");
        }
        public Dictionary<ulong, SocketRole> GetDictionarySelectionRoles()
        {
            return AvailableForSelectionRoles;
        }
        public Dictionary<ulong, SocketRole> GetColorNameDictionaryForCheck()
        {
            return SwitchColorRoles;
        }

    }
}
