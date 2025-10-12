using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._3_Infrastructure.JsonModels.Pictures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordPicturesProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordPicturesProvider> logger;
        private readonly string filePath;
        private RootDiscordPictures? RootDiscordPictures { get; set; }

        #region Aliases

        public string? PinterestPictureForAuMessageLink => RootDiscordPictures.Pinterest.ForMessage.AuMessage;
        public string? PinterestPictureForAutoLobbyNamingMessageLink => RootDiscordPictures.Pinterest.ForMessage.AutoLobbyNamingMessage;
        public string? PinterestPictureForRulesLink => RootDiscordPictures.Pinterest.ForMessage.RulesBanner;
        public string? PinterestPictureForServerPeculiaritiesImg => RootDiscordPictures.Pinterest.ForMessage.ServerPeculiaritiesImg;

        #endregion

        public JsonDiscordPicturesProvider(string filePath, ILogger<JsonDiscordPicturesProvider> logger)
        {
            this.logger = logger;
            this.filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordPictures = JsonConvert.DeserializeObject<RootDiscordPictures>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}