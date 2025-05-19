using MlkAdmin.Infrastructure.JsonModels.Pictures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MlkAdmin.Core.Providers.JsonProvider
{
    public class JsonDiscordPicturesProvider
    {
        public RootDiscordPictures? RootDiscordPictures { get; set; }

        public JsonDiscordPicturesProvider(string filePath, ILogger<JsonDiscordPicturesProvider> logger)
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