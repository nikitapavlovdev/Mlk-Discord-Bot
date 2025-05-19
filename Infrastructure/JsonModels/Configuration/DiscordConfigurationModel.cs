using Newtonsoft.Json;

namespace MlkAdmin.Infrastructure.JsonModels.Configuration
{
    public class RootDiscordConfiguration
    {
        [JsonProperty(nameof(MalenkieAdminBot))]
        public MalenkieAdminBot? MalenkieAdminBot { get; set; }

        [JsonProperty(nameof(DevelopersData))]
        public DevelopersData? DevelopersData { get; set; }

        [JsonProperty(nameof(Guild))]
        public Guild? Guild { get; set; }
    }

    public class MalenkieAdminBot
    {
        [JsonProperty(nameof(API_KEY))]
        public string? API_KEY { get; set; }
    }

    public class DevelopersData
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(IconLink))]
        public string? IconLink { get; set; }
    }

    public class Guild
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Discription))]
        public string? Discription { get; set; }
    }
}
