using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._3_Infrastructure.JsonModels.Discord.Roles;
using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordRolesListProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordRolesListProvider> logger;
        private readonly string filePath;

        private List<RoleDto>? RoleDtoList { get; set; } = [];

        public JsonDiscordRolesListProvider (string filePath, ILogger<JsonDiscordRolesListProvider> logger)
        {
            this.logger = logger;
            this.filePath = filePath;
            Load();
        }
        public void Load()
        {
            try
            {
                RoleDtoList = JsonConvert.DeserializeObject<RoleListModel>(File.ReadAllText(filePath)).Roles;
            }
            catch (Exception ex)
            { 
                logger.LogError(ex, "Ошибка чтения JSON файла ролей Discord: {Message}", ex.Message);
            }
        }

        public List<RoleDto> GetRoles()
        {
            if (RoleDtoList == null || RoleDtoList.Count == 0)
            {
                throw new InvalidOperationException("Роли не загружены или список пуст.");
            }

            return RoleDtoList;
        }
    }
}
