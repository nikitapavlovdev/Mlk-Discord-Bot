using Discord.WebSocket;

namespace MlkAdmin._1_Domain.Interfaces.Channels
{
    public interface IChannelsService
    {
        public Task<SocketTextChannel?> GetTextChannelAsync(ulong channelId);
    }
}
