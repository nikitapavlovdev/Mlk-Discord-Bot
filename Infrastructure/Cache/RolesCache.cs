using Discord;
using Discord.WebSocket;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Infrastructure.Cache
{
    public class RolesCache(
        JsonDiscordRolesProvider jsonDiscordRolesProvider,
        EmotesCache emotesCache)
    {
        const string invisSumbol = "ㅤ";

        private readonly Dictionary<ulong, SocketRole> GuildRoles = [];
        private readonly Dictionary<ulong, SocketRole> HierarchyRoles = [];
        private readonly Dictionary<ulong, SocketRole> CategoryRoles = [];
        private readonly Dictionary<ulong, SocketRole> UniqieRoles = [];
        private readonly Dictionary<ulong, SocketRole> SwitchColorRoles = [];
        private readonly Dictionary<ulong, string> RolesDescriptions = [];

        #region Action
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
            string textDescription = $"В данном блоке представлены все основные роли нашего сервера. " +
                $"Что-то можно выбрать самостоятельно, " +
                $"а что-то получить лично по желанию/на усмотрение администрации!\n";

            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            textDescription += "### иᴇᴩᴀᴩхия ᴄᴇᴩʙᴇᴩᴀ\n";

            foreach (var role in HierarchyRoles)
            {
                textDescription += $"{pointEmote} {role.Value.Mention} 🠒 {RolesDescriptions[role.Key]}\n";
            }

            textDescription += "### ᴋᴀᴛᴇᴦоᴩии\n";

            foreach (var role in CategoryRoles)
            {
                textDescription += $"{pointEmote} {role.Value.Mention} 🠒 {RolesDescriptions[role.Key]}\n";
            }

            textDescription += "### униᴋᴀᴧьныᴇ ᴩоᴧи\n";

            foreach (var role in UniqieRoles)
            {
                textDescription += $"{pointEmote} {role.Value.Mention} 🠒 {RolesDescriptions[role.Key]}\n";
            }

            return textDescription;
        }
        public string GetDescriptionForSwitchColorRoles()
        {
            string textDescription = $"В выпадающем меню снизу вы можете выбрать понравившийся цвет для вашего **сервер-нейма**!\n";

            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            textDescription += "### доᴄᴛуᴨныᴇ цʙᴇᴛᴀ\n\n";

            foreach (var role in SwitchColorRoles)
            {
                textDescription += $"> {role.Value.Mention}\n";
            }

            return textDescription;
        }
        public string GetDescriptionForRules()
        {
            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            string textDescription =
                $"{pointEmote} Внимательно прочтите правила ниже.\n" +
                $"{pointEmote} Будьте искренними с самим собой и вашими собеседниками.\n" +
                $"{pointEmote} Не засоряйте тематические каналы информационным мусором, который никак не связан с темой канала.\n" +
                $"{pointEmote} Постарайтесь уважительно относиться к точке зрения собеседника - у всех нас разный опыт за плечами.\n" +
                $"{pointEmote} Не осуждайте человека за его ошибки. Постарайтесь понять корень проблемы прежде чем делать выводы.\n" +
                $"{pointEmote} Не обсуждайте мировую политику и не создавайте ситуационных споров на этой почве.\n" +
                $"{pointEmote} Постарайтесь не выливать весь негатив на ваших собеседников. Либо делайте это, но с заранее выключеным микрофоном.\n" +
                $"{pointEmote} Будьте самими собою\n" +
                $"{pointEmote} Не стесняйтесь просить помощи у других.\n" +
                $"{pointEmote} Не стоит быть чересчур навязчивым\n" +
                $"{pointEmote} А это правило существует, чисто чтобы проверить команду!\n\n";

            textDescription += "И самое главное - наслаждайтесь моментом!";

            return textDescription;
        }
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
        public Dictionary<ulong, SocketRole> GetSwitchColorDictionary()
        {
            return SwitchColorRoles;
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
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Twitch.Id || 
                role.Id == jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Gus.Id)
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
