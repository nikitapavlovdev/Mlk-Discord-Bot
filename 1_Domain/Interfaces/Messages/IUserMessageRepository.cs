using MlkAdmin._1_Domain.Entities;
using MlkAdmin._2_Application.DTOs.Discord.Responses;

namespace MlkAdmin._1_Domain.Interfaces.Messages
{
    public interface IUserMessageRepository
    {
        public Task<DefaultResponse> AddOrUpdateAsync(UserMessagesStat stat);
        public Task<UserMessagesStat?> GetByIdAsync(ulong userId);
        public Task<List<UserMessagesStat>> GetAllAsync();
    }
}
