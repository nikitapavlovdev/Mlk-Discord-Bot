using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MlkAdmin._1_Domain.Enums;

namespace MlkAdmin._3_Infrastructure.JsonModels.Discord.Roles
{
    public class RoleDto
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }

        [JsonProperty(nameof(Type))]
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleType Type { get; set; }
    }
}
