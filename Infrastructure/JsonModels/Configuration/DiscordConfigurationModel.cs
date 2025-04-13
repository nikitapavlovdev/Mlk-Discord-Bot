using Newtonsoft.Json;

namespace Discord_Bot.Infrastructure.JsonModels.Configuration
{
    public class RootDiscordConfiguration
    {
        [JsonProperty(nameof(MalenkieAdminBot))]
        public MalenkieAdminBot? MalenkieAdminBot { get; set; }
    }

    public class MalenkieAdminBot
    {
        [JsonProperty(nameof(API_KEY))]
        public string? API_KEY { get; set; }
    }
}
