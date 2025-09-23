using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin._3_Infrastructure.JsonModels.Roles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordRolesProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordRolesProvider> logger;
        private readonly string filePath;
        public RootDiscordRoles? RootDiscordRoles { get; set; }

        #region Aliases
        public ulong AdminRoleId => RootDiscordRoles.GeneralRole.Hierarchy.Moderator.Id;
        public ulong HeadRoleId => RootDiscordRoles.GeneralRole.Hierarchy.MalenkiyHead.Id;
        #endregion
        public JsonDiscordRolesProvider(string filePath, ILogger<JsonDiscordRolesProvider> logger)
        {
            this.logger = logger;
            this.filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordRoles = JsonConvert.DeserializeObject<RootDiscordRoles>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}