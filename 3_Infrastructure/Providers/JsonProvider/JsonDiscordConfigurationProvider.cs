using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._3_Infrastructure.JsonModels.Configuration;
using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordConfigurationProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordConfigurationProvider> logger;
        private readonly string filePath;
        private RootDiscordConfiguration? RootDiscordConfiguration { get; set; }

        #region Aliases
        public ulong GuildId => RootDiscordConfiguration.Guild.Id;
        public string? DevName => RootDiscordConfiguration.DevelopersData.Name;
        public string? DevIconUrl => RootDiscordConfiguration.DevelopersData.IconLink;
        public string? ApiKey =>  RootDiscordConfiguration.MalenkieAdminBot.API_KEY;
        #endregion

        public JsonDiscordConfigurationProvider(string filePath, ILogger<JsonDiscordConfigurationProvider> logger)
        {
            this.logger = logger;
            this.filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordConfiguration = JsonConvert.DeserializeObject<RootDiscordConfiguration>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}