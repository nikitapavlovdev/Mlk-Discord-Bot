using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin.Infrastructure.Cache
{
    public class RolesCache(
        ILogger<RolesCache> logger,
        JsonDiscordRolesProvider jsonDiscordRolesProvider)
    {
        const string invisSumbol = "ㅤ";

        private readonly Dictionary<ulong, SocketRole> GuildRoles = [];
        private readonly Dictionary<ulong, SocketRole> HierarchyRoles = [];
        private readonly Dictionary<ulong, SocketRole> CategoryRoles = [];
        private readonly Dictionary<ulong, SocketRole> UniqieRoles = [];
        private readonly Dictionary<ulong, SocketRole> SwitchColorRoles = [];
        private readonly Dictionary<ulong, string> RolesDescriptions = [];

        #region Action

        public Dictionary<ulong, SocketRole> GetSwitchColorDictionary()
        {
            return SwitchColorRoles;
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

        public Dictionary<ulong, SocketRole> GetDictonaryWithRoles(RolesDictionaryType type)
        {
            try
            {
                return type switch
                {
                    RolesDictionaryType.Guild => GuildRoles,
                    RolesDictionaryType.Hierarchy => HierarchyRoles,
                    RolesDictionaryType.Category => CategoryRoles,
                    RolesDictionaryType.Unique => UniqieRoles,
                    RolesDictionaryType.SwitchColor => SwitchColorRoles,
                    _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown type: {type}"),
                };
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                return [];
            }
        }
        public Dictionary<ulong, string> GetDescriptionsForRoles()
        {
            return RolesDescriptions;
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
