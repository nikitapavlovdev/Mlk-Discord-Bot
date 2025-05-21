using MlkAdmin.Infrastructure.JsonModels.Channels;
using Newtonsoft.Json;

namespace MlkAdmin.Infrastructure.JsonModels.DynamicMessages
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

        [JsonProperty(nameof(ServerHub))]
        public ServerHub? ServerHub { get; set; }
    }

    public class ServerHub
    {
        [JsonProperty(nameof(AutoLobbyName))]
        public AutoLobbyName? AutoLobbyName { get; set; }

        [JsonProperty(nameof(Guide))]
        public Guide? Guide { get; set; }

        [JsonProperty(nameof(Autorization))]
        public Autorization? Autorization { get; set; }
    }

    public class Roles
    {
        [JsonProperty(nameof(MainRoles))]
        public MainRoles? MainRoles { get; set; }

        [JsonProperty(nameof(SwitchColor))]
        public SwitchColor? SwitchColor { get; set; }

        [JsonProperty(nameof(Rules))]
        public Rules? Rules { get; set; }    
    }

    public class Guide
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }
    public class AutoLobbyName
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }
    public class Autorization
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
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

    public class Rules
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }

    public class Hub
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }
}
