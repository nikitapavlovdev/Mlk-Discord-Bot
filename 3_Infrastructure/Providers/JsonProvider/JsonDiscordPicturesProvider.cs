using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin.Infrastructure.JsonModels.Pictures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MlkAdmin.Infrastructure.JsonModels.Emotes;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordPicturesProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordPicturesProvider> _logger;
        private readonly string _filePath;
        public RootDiscordPictures? RootDiscordPictures { get; set; }

        public JsonDiscordPicturesProvider(string filePath, ILogger<JsonDiscordPicturesProvider> logger)
        {
            _logger = logger;
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordPictures = JsonConvert.DeserializeObject<RootDiscordPictures>(File.ReadAllText(_filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}