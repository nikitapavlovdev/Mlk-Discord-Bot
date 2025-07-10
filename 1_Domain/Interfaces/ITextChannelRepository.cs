using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._1_Domain.Interfaces
{
    public interface ITextChannelRepository
    {
        Task UpsertDbTextChannelAsync(TextChannel channel);
        Task RemoveDbTextChannelAsync(ulong id);
    }
}
