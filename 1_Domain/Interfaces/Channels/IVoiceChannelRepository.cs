using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._1_Domain.Interfaces.Channels
{
    public interface IVoiceChannelRepository
    {
        Task UpsertDbVoiceChannelAsync(VoiceChannel channel);
        Task RemoveDbVoiceChannelAsync(ulong id);
        Task<bool> IsTemporaryVoiceChannel(ulong id);
        Task<bool> IsGeneratingVoiceChannel(ulong id);

    }
}
