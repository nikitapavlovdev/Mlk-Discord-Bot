using System.Text.Json;
using Newtonsoft.Json;

namespace MlkAdmin.Infrastructure.JsonModels.Users
{
    public class RootDiscordUsersLobby
    {
        [JsonProperty(nameof(User))]
        public User? User { get; set; }
    }

    public class User
    {
        [JsonProperty(nameof(GuzMan))]
        public GuzMan? GuzMan { get; set; }

        [JsonProperty(nameof(Ronin))]
        public Ronin? Ronin { get; set; }

    }

    public class GuzMan
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(LobbyName))]
        public string? LobbyName { get; set; }
    }

    public class Ronin 
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(LobbyName))]
        public string? LobbyName { get; set; }
    }
}
