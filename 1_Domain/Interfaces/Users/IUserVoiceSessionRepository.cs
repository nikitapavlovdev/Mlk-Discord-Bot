using MlkAdmin._1_Domain.Entities;
using MlkAdmin._2_Application.DTOs.Discord.Responses;

namespace MlkAdmin._1_Domain.Interfaces.Users
{
    public interface IUserVoiceSessionRepository
    {
        public Task<DefaultResponse> AddOrUpdateAsync(UserVoiceSession voiceSession); 
    }
}
