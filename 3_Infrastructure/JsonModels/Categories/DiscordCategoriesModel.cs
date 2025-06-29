using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.JsonModels.Categories
{
    public class RootDiscordCategories 
    {
        [JsonProperty(nameof(Guild))]
        public Guild? Guild { get; set; }
    }

    public class Guild
    {
        [JsonProperty(nameof(Server))]
        public Server? Server { get; set; }

        [JsonProperty(nameof(Administration))]
        public Administration? Administration { get; set; }

        [JsonProperty(nameof(General))]
        public General? General { get; set; }

        [JsonProperty(nameof(Useful))]
        public Useful? Useful { get; set; }

        [JsonProperty(nameof(Gaming))]
        public Gaming? Gaming { get; set; }

        [JsonProperty(nameof(Programming))]
        public Programming? Programming { get; set; }

        [JsonProperty(nameof(Autolobby))]
        public Autolobby? Autolobby { get; set; }

        [JsonProperty(nameof(Chill))]
        public Chill? Chill { get; set; }
    }
    public class Server
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Administration
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class General
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Useful
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Gaming
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Programming
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Autolobby
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Chill
    {
        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
}
