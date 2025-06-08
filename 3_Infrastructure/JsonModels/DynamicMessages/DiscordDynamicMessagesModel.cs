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
        [JsonProperty(nameof(ServerHub))]
        public ServerHub? ServerHub { get; set; }

        [JsonProperty(nameof(Roles))]
        public Roles? Roles { get; set; }

        [JsonProperty(nameof(Rules))]
        public Rules? Rules { get; set; }
    }

    public class ServerHub
    {
        [JsonProperty(nameof(Autorization))]
        public Autorization? Autorization { get; set; }

        [JsonProperty(nameof(Features))]
        public Features? Features { get; set; }
    }

    public class Autorization
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }

    public class Features
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }

    public class Rules
    {
        [JsonProperty(nameof(Id))]
        public ulong Id{ get; set; }
    }


    public class Roles
    {
        [JsonProperty(nameof(Main))]
        public Main? Main { get; set; }

        [JsonProperty(nameof(NameColor))]
        public NameColor? NameColor { get; set; }
    }

    public class Main
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }

    public class NameColor
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
    }
}
