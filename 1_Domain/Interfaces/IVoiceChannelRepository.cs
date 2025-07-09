using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._1_Domain.Interfaces
{
    public interface IVoiceChannelRepository
    {
        Task UpsertDbVoiceChannelAsync(VoiceChannel channel);
        Task RemoveDbVoiceChannelAsync(ulong id);
    }
}
