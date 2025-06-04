using MlkAdmin.Infrastructure.JsonModels.DynamicMessages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordDynamicMessagesProvider
    {
        public RootDynamicMessages? DynamicMessages { get; set; }

        public JsonDiscordDynamicMessagesProvider(string filePath, ILogger<JsonDiscordDynamicMessagesProvider> logger)
        {
            try
            {
                DynamicMessages = JsonConvert.DeserializeObject<RootDynamicMessages>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
