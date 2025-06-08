using Newtonsoft.Json;
using MlkAdmin.Infrastructure.JsonModels.Channels;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonChannelsMapProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonChannelsMapProvider> _logger;
        private readonly string _filePath;
        public RootChannel? RootChannel { get; set; }

        public JsonChannelsMapProvider(string filePath, ILogger<JsonChannelsMapProvider> logger)
        {
            _logger = logger;
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootChannel = JsonConvert.DeserializeObject<RootChannel>(File.ReadAllText(_filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
