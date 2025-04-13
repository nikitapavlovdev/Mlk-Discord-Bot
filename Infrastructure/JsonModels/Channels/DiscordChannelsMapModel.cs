using Newtonsoft.Json;

namespace Discord_Bot.Infrastructure.JsonModels.Channels
{
    public class RootChannel
    {
        [JsonProperty(nameof(Channels))]
        public Channels? Channels { get; set; }
    }
    public class Channels
    {
        [JsonProperty(nameof(VoiceChannels))]
        public VoiceChannels? VoiceChannels { get; set; }

        [JsonProperty(nameof(TextChannels))]
        public TextChannels? TextChannels { get; set; }
    }
    public class VoiceChannels
    {
        [JsonProperty(nameof(AutoLobby))]
        public AutoLobby? AutoLobby { get; set; }

        [JsonProperty(nameof(MainLobby))]
        public MainLobby? MainLobby { get; set; }
    }
    public class AutoLobby
    {
        [JsonProperty(nameof(AutoGamesLobby))]
        public AutoGamesLobby? AutoGamesLobby { get; set; }
    }
    public class AutoGamesLobby
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(IsGeneratingChannel))]
        public bool IsGeneratingChannel { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }
    }
    public class MainLobby
    {
        [JsonProperty(nameof(GeneralLobby))]
        public GeneralLobby? GeneralLobby { get; set; }

        [JsonProperty(nameof(AdminLobby))]
        public AdminLobby? AdminLobby { get; set; }
    }
    public class GeneralLobby
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(IsGeneratingChannel))]
        public bool IsGeneratingChannel { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }
    }
    public class AdminLobby
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(IsGeneratingChannel))]
        public bool IsGeneratingChannel { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }
    }
    public class TextChannels
    {
        [JsonProperty(nameof(ServerCategory))]
        public ServerCategory? ServerCategory { get; set; }

        [JsonProperty(nameof(AdministratorCategory))]
        public AdministratorCategory? AdministratorCategory { get; set; }

        [JsonProperty(nameof(BaseCategory))]
        public BaseCategory? BaseCategory { get; set; }

        [JsonProperty(nameof(GameCategory))]
        public GameCategory? GameCategory { get; set; }

        [JsonProperty(nameof(ProgrammingCategory))]
        public ProgrammingCategory? ProgrammingCategory { get; set; }
    }
    public class ServerCategory
    {
        [JsonProperty(nameof(Rules))]
        public Rules? Rules { get; set; }

        [JsonProperty(nameof(News))]
        public News? News { get; set; }

        [JsonProperty(nameof(BotCommands))]
        public BotCommands? BotCommands { get; set; }

        [JsonProperty(nameof(Roles))]
        public Roles? Roles { get; set; }

        [JsonProperty(nameof(Starting))]
        public Starting? Starting { get; set; }
    }
    public class Rules
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class News
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class BotCommands
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Starting
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Roles
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class AdministratorCategory
    {
        [JsonProperty(nameof(Chat))]
        public Chat? Chat { get; set; }

        [JsonProperty(nameof(Logs))]
        public Logs? Logs { get; set; }
    }
    public class Chat
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class Logs
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class BaseCategory
    {
        [JsonProperty(nameof(Chat))]
        public Chat? Chat { get; set; }
    }
    public class GameCategory
    {
        [JsonProperty(nameof(Highlight))]
        public Highlight? Highlight { get; set; }

        [JsonProperty(nameof(ValChat))]
        public ValChat? ValChat { get; set; }
    }
    public class Highlight
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class ValChat
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
    public class ProgrammingCategory
    {
        [JsonProperty(nameof(Chat))]
        public Chat? Chat { get; set; }

        [JsonProperty(nameof(WebHook))]
        public WebHook? WebHook { get; set; }
    }
    public class WebHook
    {
        [JsonProperty(nameof(Name))]
        public string? Name { get; set; }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; set; }

        [JsonProperty(nameof(Https))]
        public string? Https { get; set; }

        [JsonProperty(nameof(Description))]
        public string? Description { get; set; }
    }
}
