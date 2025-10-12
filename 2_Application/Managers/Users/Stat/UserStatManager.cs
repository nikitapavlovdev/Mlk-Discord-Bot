using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._2_Application.Managers.Users.Stat
{
    public class UserStatManager(
        ILogger<UserStatManager> logger, 
        IUserMessageRepository userMessageRepository)
    {

        public async Task IncrementMessageCountAsync(ulong userId)
        {
            try
            {
                await userMessageRepository.AddOrUpdateAsync(new UserMessagesStat()
                {
                    Id = userId,
                    Count = 1,
                    LastUpdate = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке увеличить счетчик пользователя {userId}", userId);
            }
        }

    }
}
