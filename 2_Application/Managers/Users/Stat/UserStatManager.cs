using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces.Users;
using Discord.WebSocket;

namespace MlkAdmin._2_Application.Managers.Users.Stat
{
    public class UserStatManager(
        ILogger<UserStatManager> logger, 
        IUserMessageRepository userMessageRepository,
        IUserVoiceSessionRepository userVoiceSessionRepository)
    {
        public async Task IncrementMessageCountAsync(ulong userId)
        {
            try
            {
                await userMessageRepository.AddOrUpdateAsync(new UserMessagesStat()
                {
                    UserId = userId,
                    Count = 1,
                    LastUpdate = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке увеличить счетчик пользователя");
            }
        }

        public async Task TrackUserVoiceSessionsAsync(ulong userId, SocketVoiceState newState, SocketVoiceState oldState)
        {
            try
            {
                if (oldState.VoiceChannel != null && newState.VoiceChannel == null)
                {
                    await userVoiceSessionRepository.AddOrUpdateAsync(new UserVoiceSession()
                    {
                        UserId = userId,
                        VoiceStarting = null
                    });
                }

                if (oldState.VoiceChannel == null && newState.VoiceChannel != null)
                {
                    await userVoiceSessionRepository.AddOrUpdateAsync(new UserVoiceSession()
                    {
                        UserId = userId,
                        VoiceStarting = DateTime.UtcNow
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке отследить голосовую сессию");
            }
        }
    }
}
