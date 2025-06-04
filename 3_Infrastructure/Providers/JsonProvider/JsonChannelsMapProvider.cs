using Newtonsoft.Json;
using MlkAdmin.Infrastructure.JsonModels.Channels;
using Microsoft.Extensions.Logging;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonChannelsMapProvider
    {
        public RootChannel? RootChannel { get; set; }
        
        public JsonChannelsMapProvider(string filePath, ILogger<JsonChannelsMapProvider> logger)
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
