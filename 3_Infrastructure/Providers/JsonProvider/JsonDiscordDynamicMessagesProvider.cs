using MlkAdmin.Infrastructure.JsonModels.DynamicMessages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MlkAdmin._1_Domain.Interfaces;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordDynamicMessagesProvider : IJsonConfigurationProvider
    {

        private readonly ILogger<JsonDiscordDynamicMessagesProvider> _logger;
        private readonly string _filePath;
        public RootDynamicMessages? DynamicMessages { get; set; }

        public JsonDiscordDynamicMessagesProvider(string filePath, ILogger<JsonDiscordDynamicMessagesProvider> logger)
        {
            _logger = logger;
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                DynamicMessages = JsonConvert.DeserializeObject<RootDynamicMessages>(File.ReadAllText(_filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
