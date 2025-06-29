using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.JsonModels.Emotes
{
    public class RootDiscordEmotes
    {
        [JsonProperty(nameof(AnimatedEmotes))]
        public AnimatedEmotes? AnimatedEmotes { get; set; }

        [JsonProperty(nameof(StaticEmotes))]
        public StaticEmotes? StaticEmotes { get; set; }
    }

    public class AnimatedEmotes
    {
        [JsonProperty(nameof(GeneralAnimatedDesignations))]
        public GeneralAnimatedDesignations? GeneralAnimatedDesignations { get; set; }

        [JsonProperty(nameof(AnimatedZero))]
        public AnimatedZero? AnimatedZero { get; set; }
    }
    
    public class GeneralAnimatedDesignations
    {
        [JsonProperty(nameof(AnimatedGreenCheck))]
        public AnimatedGreenCheck? AnimatedGreenCheck { get; set; }
    }

    public class AnimatedGreenCheck
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Discription))]
        public string? Discription { get; set; }
    }

    public class AnimatedZero
    {
        [JsonProperty(nameof(Peaceout))]
        public Peaceout? Paceout { get; set; }
    }
    
    public class Peaceout
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Discription))]
        public string? Discription { get; set; }
    }

    public class StaticEmotes
    {
        [JsonProperty(nameof(StaticZero))]
        public StaticZero? StaticZero { get; set; }
        [JsonProperty(nameof(Paimon))]
        public Paimon? Paimon { get; set; }
    }

    public class StaticZero
    {
        [JsonProperty(nameof(Love))]
        public Love? Love { get; set; }

        [JsonProperty(nameof(Hmph))]
        public Hmph? Hmph { get; set; }
    }

    public class Love
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Discription))]
        public string? Discription { get; set; }
    }

    public class Hmph
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Discription))]
        public string? Discription { get; set; }
    }

    public class Paimon
    {
        [JsonProperty(nameof(Little))]
        public Little? Little { get; set; }
    }

    public class Little
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Discription))]
        public string? Discription { get; set; }
    }

    public class GeneralStaticDesignations
    {
        [JsonProperty(nameof(StaticGreenCheck))]
        public StaticGreenCheck? StaticGreenCheck { get; set; }
        [JsonProperty(nameof(StaticCross))]
        public StaticCross? StaticCross { get; set; }
    }

    public class StaticGreenCheck
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
        [JsonProperty(nameof(Discription))]
        public string? Discription { get; set; }
    }

    public class StaticCross
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }
        [JsonProperty(nameof(Discription))]
        public string? Discription { get; set; }
    }
}
