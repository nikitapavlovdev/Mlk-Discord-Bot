using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._1_Domain.Interfaces.Channels
{
    public interface ITextChannelsRepository
    {
        Task UpsertDbTextChannelAsync(TextChannel channel);
        Task RemoveDbTextChannelAsync(ulong id);
    }
}
