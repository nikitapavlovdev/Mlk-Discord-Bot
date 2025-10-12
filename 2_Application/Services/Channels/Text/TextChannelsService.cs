using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._3_Infrastructure.Cache.Channels;

namespace MlkAdmin._2_Application.Services.Channels
{
    public class TextChannelsService(
        ILogger<TextChannelsService> logger, 
        ChannelsCache channelsCache) : IChannelsService
    {
        public async Task<SocketTextChannel?> GetTextChannelAsync(ulong channelId)
        {
            try
            {
                if (channelId <= 0)
                    throw new ArgumentNullException(nameof(channelId), "channelId не может быть нулем или отрицательным числом");

                Dictionary<ulong, SocketGuildChannel> Channels = await channelsCache.GetChannelsAsync();

                if (!Channels.TryGetValue(channelId, out SocketGuildChannel? guildChannel))
                {
                    logger.LogWarning("Канал с ID {channelId} не найден в кеше", channelId);
                    return null;    
                }

                return guildChannel as SocketTextChannel;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке получить канал");
                throw;
            }
        }
    }
}
