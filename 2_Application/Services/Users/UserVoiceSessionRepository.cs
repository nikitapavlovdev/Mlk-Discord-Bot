using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces.Users;
using MlkAdmin._2_Application.DTOs.Discord.Responses;
using MlkAdmin._3_Infrastructure.DataBase;

namespace MlkAdmin._2_Application.Services.Users
{
    public class UserVoiceSessionRepository(
        ILogger<UserVoiceSessionRepository> logger,
        MlkAdminDbContext dbContext) : IUserVoiceSessionRepository
    {
        public async Task<DefaultResponse> AddOrUpdateAsync(UserVoiceSession voiceSession)
        {
            try
            {
                UserVoiceSession? session = await dbContext.VoiceSession.FindAsync(voiceSession.UserId);

                if (session is null)
                {
                    if (voiceSession.VoiceStarting.HasValue)
                    {
                        await dbContext.VoiceSession.AddAsync(voiceSession);
                        await dbContext.SaveChangesAsync();

                        return new DefaultResponse()
                        {
                            IsSuccess = true,
                            Message = "Добавлена новая запись по сессиям"
                        };
                    }
                    else
                    {
                        return new DefaultResponse()
                        {
                            IsSuccess = false,
                            Message = "Пользователь вышел из голосового во время перезапуска бота"
                        };  
                    }
                }

                if (session.VoiceStarting.HasValue && !voiceSession.VoiceStarting.HasValue)
                {
                    session.TotalSeconds += (long)(DateTime.UtcNow - session.VoiceStarting.Value).TotalSeconds;

                    dbContext.Update(session);
                    await dbContext.SaveChangesAsync();

                    return new DefaultResponse()
                    {
                        IsSuccess = true,
                        Message = "Обновлена запись по сессиям"
                    };
                }

                session.VoiceStarting = voiceSession.VoiceStarting;

                dbContext.VoiceSession.Update(session);
                await dbContext.SaveChangesAsync();

                return new DefaultResponse()
                {
                    IsSuccess = true,
                    Message = "Обновлена запись по сессиям"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке добавить или обновить голосовую сессию пользователя");

                return new DefaultResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
        public async Task<long> GetVoiceSpendTimeAsync(ulong userId)
        {
            UserVoiceSession? session = await dbContext.VoiceSession.FirstAsync(x => x.UserId == userId);

            if (session == null)
            {
                return 0;
            }

            return session.TotalSeconds;
        }
    }
}
