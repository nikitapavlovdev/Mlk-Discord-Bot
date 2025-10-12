using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.JsonModels.Discord.Roles
{
    public class RoleListModel
    {
        [JsonProperty("RolesWithDicriptions")]
        public List<RoleDto> Roles { get; set; } = [];
    }
}
