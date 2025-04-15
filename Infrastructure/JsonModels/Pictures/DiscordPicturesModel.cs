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
    }
}
