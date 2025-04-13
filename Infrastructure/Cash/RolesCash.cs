using Discord.WebSocket;
using Discord_Bot.Core.Utilities.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Infrastructure.Cash
{
    public class RolesCash(IConfiguration configuration, EmotesCash emotesCash, ILogger<RolesCash> _logger)
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

                    if(role.Id == ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Lime:Id"])||
                       role.Id == ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Khaki:Id"])||
                       role.Id == ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Violet:Id"])||
                       role.Id == ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Crimson:Id"])||
                       role.Id == ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Slateblue:Id"])||
                       role.Id == ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Coral:Id"]))
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

            BaseRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:MalenkiyHead:Id"]),
               ExtensionMethods.GetStringFromConfiguration(configuration["Roles:MalenkiyHead:Description"]));

            BaseRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:Moderator:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["Roles:Moderator:Description"]));

            BaseRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:MalenkiyMember:Id"]),  
                ExtensionMethods.GetStringFromConfiguration(configuration["Roles:MalenkiyMember:Description"]));



            GamesRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:DestinyEnjoyer:Id"]), 
                ExtensionMethods.GetStringFromConfiguration(configuration["Roles:DestinyEnjoyer:Description"]));

            GamesRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:Valoranter:Id"]), 
                ExtensionMethods.GetStringFromConfiguration(configuration["Roles:Valoranter:Description"]));



            AnotherRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:IKIT:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["Roles:IKIT:Description"]));

            AnotherRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:International:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["Roles:International:Description"]));

            AnotherRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:InformationHunter:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["Roles:InformationHunter:Description"]));

            AnotherRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:DeadInside:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["Roles:DeadInside:Description"]));



            ServerBoosterRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Coral:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Coral:Description"]));

            ServerBoosterRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Khaki:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Khaki:Description"]));

            ServerBoosterRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Violet:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Violet:Description"]));

            ServerBoosterRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Crimson:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Crimson:Description"]));

            ServerBoosterRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Slateblue:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Slateblue:Description"]));

            ServerBoosterRolesDescriptions.TryAdd(ExtensionMethods.ConvertId(configuration["ServerBoostRoles:Lime:Id"]),
                ExtensionMethods.GetStringFromConfiguration(configuration["ServerBoostRoles:Lime:Description"]));



            RolesAvailableForSelection.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:DestinyEnjoyer:Id"]),
                GetRole(ExtensionMethods.ConvertId(configuration["Roles:DestinyEnjoyer:Id"])));

            RolesAvailableForSelection.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:InformationHunter:Id"]),
                GetRole(ExtensionMethods.ConvertId(configuration["Roles:InformationHunter:Id"])));

            RolesAvailableForSelection.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:Valoranter:Id"]),
                GetRole(ExtensionMethods.ConvertId(configuration["Roles:Valoranter:Id"])));

            RolesAvailableForSelection.TryAdd(ExtensionMethods.ConvertId(configuration["Roles:IKIT:Id"]),
                GetRole(ExtensionMethods.ConvertId(configuration["Roles:IKIT:Id"])));

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
                $"\nВ выпадающем списке просто выбери то, что тебе интересно {emotesCash.GetEmote(ExtensionMethods.ConvertId(configuration["static:zero_love:id"]))}\n" +
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
