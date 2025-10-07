using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._3_Infrastructure.Cache.Users;

namespace MlkAdmin._2_Application.Services.Users
{
    public class UserService(
        ILogger<UserService> logger, 
        UsersCache usersCache)
    {
        public SocketGuildUser? GetGuildUser(ulong userId)
        {
            try
            {
                return usersCache.GetAllUsers().GetOrAdd(userId, _=> null!);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка получения пользователя");
                return null;
            }
        }
    }
}