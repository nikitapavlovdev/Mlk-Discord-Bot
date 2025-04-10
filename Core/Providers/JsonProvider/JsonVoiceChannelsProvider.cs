using Newtonsoft.Json;
using Discord_Bot.Infrastructure.JsonModels.Channels;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Core.Providers.JsonProvider
{
    public class JsonVoiceChannelsProvider(string _filePath, ILogger<JsonVoiceChannelsProvider> _logger)
    {
        public RootVoiceChannels? GetRootChannels()
        {
			try
			{
                return JsonConvert.DeserializeObject<RootVoiceChannels>(File.ReadAllText(_filePath));
			}
			catch (Exception ex)
			{
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                return null;
            }
        } 
    }
}
