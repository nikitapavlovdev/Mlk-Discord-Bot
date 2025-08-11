using Newtonsoft.Json;
using MlkAdmin._3_Infrastructure.JsonModels.Channels;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordChannelsMapProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordChannelsMapProvider> logger;
        private readonly string filePath;
        private RootChannel? RootChannel { get; set; }

        #region Aliases
        public ulong LogsChannelId => RootChannel.Channels.TextChannels.AdministratorCategory.Logs.Id;
        public ulong FeedbackChannelId => RootChannel.Channels.TextChannels.AdministratorCategory.Feedback.Id;
        public ulong StartingChannelId => RootChannel.Channels.TextChannels.ServerCategory.Starting.Id;
        public ulong HubChannelId => RootChannel.Channels.TextChannels.ServerCategory.Hub.Id;
        public string? HubChannelHttps => RootChannel.Channels.TextChannels.ServerCategory.Hub.Https;
        public ulong RulesChannelId => RootChannel.Channels.TextChannels.ServerCategory.Rules.Id;
        public string? RulesChannelHttps => RootChannel.Channels.TextChannels.ServerCategory.Rules.Https;
        public ulong RolesChannelId => RootChannel.Channels.TextChannels.ServerCategory.Roles.Id;
        public string? RolesChannelHttps => RootChannel.Channels.TextChannels.ServerCategory.Roles.Https;
        public ulong AutoGameLobbyId => RootChannel.Channels.VoiceChannels.AutoLobby.AutoGamesLobby.Id;
        #endregion

        public JsonDiscordChannelsMapProvider(string filePath, ILogger<JsonDiscordChannelsMapProvider> logger)
        {
            this.logger = logger;
            this.filePath = filePath;
            Load();
        }
        public void Load()
        {
            try
            {
                RootChannel = JsonConvert.DeserializeObject<RootChannel>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
