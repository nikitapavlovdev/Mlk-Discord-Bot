using Newtonsoft.Json;

namespace Discord_Bot.Infrastructure.JsonModels.DynamicMessages
{
    public class RootDynamicMessages
    {
        [JsonProperty(nameof(Messages))]
        public Messages? Messages { get; set; }
    }

    public class Messages
    {
        [JsonProperty(nameof(Roles))]
        public Roles? Roles { get; set; }
    }

    public class Roles
    {
        [JsonProperty(nameof(MainRoles))]
        public MainRoles? MainRoles { get; set; }

        [JsonProperty(nameof(SwitchColor))]
        public SwitchColor? SwitchColor { get; set; }
    }

    public class MainRoles
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }

    public class SwitchColor
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }
}
