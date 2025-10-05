using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._3_Infrastructure.Cache.Channels
{
    public class ChannelsCache(ILogger<ChannelsCache> logger)
    {
        private readonly Dictionary<ulong, SocketGuildChannel> Channels = []; 

        public Task FillChannelsAsync(IReadOnlyCollection<SocketGuildChannel> guildChannels)
        {
            try
            {
                if (guildChannels is null)
                    throw new ArgumentNullException(nameof(guildChannels), "Переданная коллекция каналов не может быть null");

                foreach (SocketGuildChannel channel in guildChannels)
                    Channels.TryAdd(channel.Id, channel);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при заполнение каналов");
                throw;
            }
        }
        public Task<Dictionary<ulong, SocketGuildChannel>> GetChannelsAsync()
        {
            return Task.FromResult(Channels.ToDictionary(x => x.Key, x => x.Value));
        }
    }
}
