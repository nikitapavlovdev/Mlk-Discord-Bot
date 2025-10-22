using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._2_Application.DTOs.Discord.Responses;
using MlkAdmin._3_Infrastructure.DataBase;

namespace MlkAdmin._2_Application.Services.Users
{
    public class UserMessageRepository(
        MlkAdminDbContext dbContext,
        ILogger<UserMessageRepository> logger) : IUserMessageRepository
    {
        public async Task<DefaultResponse> AddOrUpdateAsync(UserMessagesStat stat)
        {
            try
            {
                UserMessagesStat? existing = await dbContext.Messages.FindAsync(stat.UserId);

                if (existing is null)
                {
                    await dbContext.Messages.AddAsync(stat);
                    await dbContext.SaveChangesAsync();

                    return new DefaultResponse()
                    {
                        IsSuccess = true,
                        Message = "Запись успешно добавлена в UserMessagesStat"
                    };
                }

                existing.Count += stat.Count;
                existing.LastUpdate = stat.LastUpdate;

                dbContext.Messages.Update(existing);
                await dbContext.SaveChangesAsync();

                return new DefaultResponse
                {
                    IsSuccess = true,
                    Message = $"Обновлён счётчик сообщений пользователя {existing.UserId}"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытки добавления / обновления записи в UserMessagesStat");
                return new DefaultResponse()
                {
                    IsSuccess = false,
                    Message = "Ошибка при попытки добавления / обновления записи в UserMessagesStat",
                    Exception = ex
                };
            }
        }
        public async Task<UserMessagesStat?> GetByIdAsync(ulong userId)
        {
            try
            {
                return await dbContext.Messages.FindAsync(userId); ;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке получения записи из UserMessagesStat по Id");
                return null;
            }
        }
        public async Task<List<UserMessagesStat>> GetAllAsync()
        {
            try
            {
                return await dbContext.Messages.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке получения всех записей из UserMessagesStat");

                return [];
            }
        }
        public async Task<int> GetMessagesNumberAsync(ulong userId)
        {
            UserMessagesStat? stat = await dbContext.Messages.FirstAsync(x => x.UserId == userId);

            if(stat == null)
            {
                return -1;
            }

            return stat.Count;
        }
    }
}
