using Newtonsoft.Json;

namespace Discord_Bot.Infrastructure.JsonModels.Pictures
{
    public class RootDiscordPictures
    {
        [JsonProperty(nameof(Pinterest))]
        public Pinterest? Pinterest { get; set; }
    }

    public class Pinterest
    {
        [JsonProperty(nameof(ForMessage))]
        public ForMessage? ForMessage { get; set; }
    }

    public class ForMessage
    {
        [JsonProperty(nameof(LinkPurpleEyes))]
        public string? LinkPurpleEyes { get; set; }

        [JsonProperty(nameof(LinkPinkEyes))]
        public string? LinkPinkEyes { get; set; }

        [JsonProperty(nameof(LinkGreenEyes))]
        public string? LinkGreenEyes { get; set; }

        [JsonProperty(nameof(BlackWhiteCloud1))]
        public string? BlackWhiteCloud1 { get; set; }

        [JsonProperty(nameof(RolesBanner))]
        public string? RolesBanner { get; set; }

        [JsonProperty(nameof(ColorNameBanner))]
        public string? ColorNameBanner { get; set; }

        [JsonProperty(nameof(RulesBanner))]
        public string? RulesBanner { get; set; }
    }
}
