using Discord;
using Discord.WebSocket;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Infrastructure.Cache
{
    public class RolesCache(
        JsonDiscordRolesProvider jsonDiscordRolesProvider,
        EmotesCache emotesCache)
    {
        private readonly Dictionary<ulong, SocketRole> GuildRoles = [];
        private readonly Dictionary<ulong, SocketRole> HierarchyRoles = [];
        private readonly Dictionary<ulong, SocketRole> CategoryRoles = [];
        private readonly Dictionary<ulong, SocketRole> UniqieRoles = [];
        private readonly Dictionary<ulong, SocketRole> SwitchColorRoles = [];
        private readonly Dictionary<ulong, string> RolesDescriptions = [];

        #region Action
        public void AddRole(SocketRole role)
        {
            GuildRoles.TryAdd(role.Id, role);

            if (IsHierarchyServerRole(role))
            {
                HierarchyRoles.TryAdd(role.Id, role);
            }

            if (IsCategoryRole(role))
            {
                CategoryRoles.TryAdd(role.Id, role);
            }

            if (IsUniqueRole(role))
            {
                UniqieRoles.TryAdd(role.Id, role);
            }

            if (IsSwitchColorRole(role))
            {
                SwitchColorRoles.TryAdd(role.Id, role);
            }
        }
        public void AddRoleDescription(ulong roleId, string roleDescription)
        {
            RolesDescriptions.TryAdd(roleId, roleDescription);
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
        public string GetDescriptionForMainRoles()
        {
            string textDescription = $"> В данном блоке представлены все основные роли нашего сервера. " +
                $"Что-то можно выбрать самостоятельно, " +
                $"а что-то получить лично по желанию/на усмотрение администрации!\n";
            
            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            textDescription += "### РОЛИ ИЕРАРХИИ\n";

            foreach (var role in HierarchyRoles)
            {
                textDescription += $"{pointEmote} {role.Value.Mention} -\t {RolesDescriptions[role.Key]}\n";
            }

            textDescription += $"### РОЛИ КАТЕГОРИИ\n";

            foreach (var role in CategoryRoles)
            {
                textDescription += $"{pointEmote} {role.Value.Mention} -\t {RolesDescriptions[role.Key]}\n";
            }

            textDescription += $"### УНИКАЛЬНЫЕ РОЛИ\n";

            foreach (var role in UniqieRoles)
            {
                textDescription += $"{pointEmote} {role.Value.Mention} -\t {RolesDescriptions[role.Key]}\n";
            }


            return textDescription;
        }
        public string GetDescriptionForSwitchColorRoles()
        {
            string textDescription = $"> В данном блоке содержатся роли, которые изменяют цвет вашего никнейма на сервере. Обратите внимание, что некоторые цвета доступны только для **Nitro-бустеров**!\n";

            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            textDescription += "### Доступные цвета\n\n";

            foreach (var role in SwitchColorRoles)
            {
                textDescription += $"{pointEmote} {role.Value.Mention}\n";
            }

            return textDescription;
        }
        #endregion

        #region Condition
        private bool IsHierarchyServerRole(SocketRole role)
        {
            if (role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.Moderator.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.MalenkiyHead.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.NotRegistered.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.ServerBooster.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsCategoryRole(SocketRole role)
        {
            if (role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Gamer.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Id)
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
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Amnyam.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.BlackBeer.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Gacha.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.LadyFlora.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Svin.Id ||
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Twitch.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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
        #endregion
    }
}
