using Discord.WebSocket;
using Discord_Bot.Core.Providers.JsonProvider;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Infrastructure.Cache
{
    public class RolesCache(
        EmotesCache emotesCash, 
        ILogger<RolesCache> _logger,
        JsonDiscordRolesProvider jsonDiscordRolesProvider,
        JsonDiscordEmotesProvider jsonDiscordEmotesProvider)
    {
        private readonly Dictionary<ulong, SocketRole> MainServerRoles = [];
        private readonly Dictionary<ulong, string> BaseRolesDescriptions = [];
        private readonly Dictionary<ulong, string> GamesRolesDescriptions = [];
        private readonly Dictionary<ulong, string> AnotherRolesDescriptions = [];
        private readonly Dictionary<ulong, string> ServerBoosterRolesDescriptions = [];
        private readonly Dictionary<ulong, SocketRole> RolesAvailableForSelection = [];
        private readonly List<SocketRole> ColorNameList = [];  

        public async Task RolesInitialization(SocketGuild socketGuild)
        {
            try
            {
                await Task.WhenAll(
                    SetRoles(socketGuild),
                    FillRolesDescriptionsDict()
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}", ex.Message);
            }
        }
        private async Task SetRoles(SocketGuild socketGuild)
        {
            try
            {
                IReadOnlyCollection<SocketRole> roles = socketGuild.Roles;

                foreach (SocketRole role in roles)
                {
                    MainServerRoles.Add(role.Id, role);

                    if(role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Lime.Id ||
                       role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Khaki.Id ||
                       role.Id == jsonDiscordRolesProvider .RootDiscordRoles.ColorSwitch.Booster.Violet.Id ||
                       role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Crimson.Id ||
                       role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Slateblue.Id ||
                       role.Id == jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Coral.Id)
                    {
                        ColorNameList.Add(role);
                    }
                }

                _logger.LogInformation("Roles has been uploaded");
                
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public SocketRole GetRole(ulong roleId)
        {
            SocketRole role = MainServerRoles[roleId];

            if (role != null)
            {
                return role;
            }

            throw new KeyNotFoundException($"Role with ID {roleId} not found.");
        }
        private async Task FillRolesDescriptionsDict()
        {
            BaseRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Description);

            BaseRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.Moderator.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.Moderator.Description);

            GamesRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.DestinyEnjoyer.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.DestinyEnjoyer.Description);

            GamesRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Valoranter.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Valoranter.Description);

            AnotherRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Description);

            AnotherRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.International.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.International.Description);

            AnotherRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Description);

            AnotherRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.DeadInside.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.DeadInside.Description);

            ServerBoosterRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Coral.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Coral.Description);

            ServerBoosterRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Khaki.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Khaki.Description);

            ServerBoosterRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Violet.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.Booster.Violet.Description);

            ServerBoosterRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Crimson.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Crimson.Description);

            ServerBoosterRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Slateblue.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Slateblue.Description);

            ServerBoosterRolesDescriptions.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Lime.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.ColorSwitch.NotBooster.Lime.Description);

            RolesAvailableForSelection.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.DestinyEnjoyer.Id,
                GetRole(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.DestinyEnjoyer.Id));

            RolesAvailableForSelection.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Id,
                GetRole(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Id));

            RolesAvailableForSelection.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Valoranter.Id,
                GetRole(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Valoranter.Id));

            RolesAvailableForSelection.TryAdd(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Id,
                GetRole(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Id));

            await Task.CompletedTask;
        }
        public string GetDescriptionInfoAboutRoles()
        {
            string description = "В данном блоке находится общая информация об основных ролях нашего сервера, " +
                "а так же блок с ролями, которые можно получить прямо сейчас\n";

            description += "## Главное\n\n";
            
            foreach (var role in BaseRolesDescriptions)
            {
                description += $"> {GetRole(role.Key).Mention} - {role.Value}\n";
            }

            description += "## Игры\n\n";

            foreach(var role in GamesRolesDescriptions)
            {
                description += $"> {GetRole(role.Key).Mention} - {role.Value}\n";
            }

            description += "## Прочее\n\n";

            foreach( var role in AnotherRolesDescriptions)
            {
                description += $"> {GetRole(role.Key).Mention} - {role.Value}\n";
            }

            return description;
        }
        public string GetDescriptionForСhoiceRoles()
        {
            string description = $"Доступные для выбора роли. " +
                $"\nВ выпадающем списке просто выбери то, что тебе интересно {emotesCash.GetEmote(jsonDiscordEmotesProvider.RootDiscordEmotes.StaticEmotes.StaticZero.Love.Id)}\n" +
                $"### Открывающие категории/каналы\n\n";

            foreach(var role in RolesAvailableForSelection)
            {
                description += $"> {role.Value.Mention}\n";
            }

            description += "### Изменение цвета имени\n\n";

            foreach( var role in ServerBoosterRolesDescriptions)
            {
                description += $"> {GetRole(role.Key).Mention}\n";
            }

            return description;
        }
        public List<SocketRole> ReturnColorNameListForCheck()
        {
            List<SocketRole> socketRoles = ColorNameList;

            return socketRoles;
        }
        public async Task DeleteAllRolesFromUser(SocketGuildUser socketGuildUser)
        {
            foreach(var role in RolesAvailableForSelection)
            {
                if(socketGuildUser.Roles.Any(userRole => userRole.Id == role.Key))
                {
                    await socketGuildUser.RemoveRoleAsync(role.Key);
                }
            }
        }
    }
}
