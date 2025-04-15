using Newtonsoft.Json;

namespace Discord_Bot.Infrastructure.JsonModels.Roles
{
    public class RootDiscordRoles
    {
        [JsonProperty(nameof(GeneralRole))]
        public GeneralRole? GeneralRole { get; set; }

        [JsonProperty(nameof(ColorSwitch))]
        public ColorSwitch? ColorSwitch { get; set; }
    }
    public class GeneralRole
    {
        [JsonProperty(nameof(Hierarchy))]
        public Hierarchy? Hierarchy { get; set; }

        [JsonProperty(nameof(Autorization))]
        public Autorization? Autorization { get; set; }

        [JsonProperty(nameof(Unique))]
        public Unique? Unique { get; set; }

        [JsonProperty(nameof(Categories))]
        public Categories? Categories { get; set; }

    }
    public class Hierarchy
    {
        [JsonProperty(nameof(MalenkiyHead))]
        public MalenkiyHead? MalenkiyHead { get; set; }

        [JsonProperty(nameof(Moderator))]
        public Moderator? Moderator { get; set; }
    }
    public class Autorization
    {
        [JsonProperty(nameof(NotRegistered))]
        public NotRegistered? NotRegistered { get; set; }

        [JsonProperty(nameof(MalenkiyMember))]
        public MalenkiyMember? MalenkiyMember { get; set; }
    }
    public class Unique
    {
        [JsonProperty(nameof(International))]
        public International? International { get; set; }

        [JsonProperty(nameof(DeadInside))]
        public DeadInside? DeadInside { get; set; }
    }
    public class Categories
    {
        [JsonProperty(nameof(DestinyEnjoyer))]
        public DestinyEnjoyer? DestinyEnjoyer { get; set; }

        [JsonProperty(nameof(InformationHunter))]
        public InformationHunter? InformationHunter { get; set; }

        [JsonProperty(nameof(Valoranter))]
        public Valoranter? Valoranter { get; set; }

        [JsonProperty(nameof(IKIT))]
        public IKIT? IKIT { get; set; }
    }
    public class MalenkiyHead
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Moderator
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class NotRegistered
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class MalenkiyMember
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class International
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class DeadInside
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class DestinyEnjoyer
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class InformationHunter
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Valoranter
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class IKIT
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class ColorSwitch
    {
        [JsonProperty(nameof(NotBooster))]
        public NotBooster? NotBooster   { get; set; }

        [JsonProperty(nameof(Booster))]
        public Booster? Booster { get; set; }
    }
    public class NotBooster
    {
        [JsonProperty(nameof(Crimson))]
        public Crimson? Crimson { get; set; }

        [JsonProperty(nameof(Slateblue))]
        public Slateblue? Slateblue { get; set; }

        [JsonProperty(nameof(Lime))]
        public Lime? Lime { get; set; }
    }
    public class Booster
    {
        [JsonProperty(nameof(Coral))]
        public Coral? Coral { get; set; }

        [JsonProperty(nameof(Khaki))]
        public Khaki? Khaki { get; set; }

        [JsonProperty(nameof(Violet))]
        public Violet? Violet { get; set; }
    }
    public class Crimson
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Slateblue
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Lime
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Coral
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Khaki
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Violet
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
}
