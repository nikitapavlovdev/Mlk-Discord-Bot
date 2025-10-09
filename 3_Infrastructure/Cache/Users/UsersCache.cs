using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.DTOs.Responses;
using System.Collections.Concurrent;

namespace MlkAdmin._3_Infrastructure.Cache.Users
{
    public class UsersCache(
        ILogger<UsersCache> logger)
    {
        private readonly ConcurrentDictionary<ulong, SocketGuildUser> GuildUsers = [];

        public Task<DefaultResponse> FillUsers(SocketGuild guild)
        {
            try
            {
                if (guild is null)
                    return Task.FromResult( new DefaultResponse()
                    {
                        IsSuccess = false,
                        Message = "Гильдия не найдена",
                        Exception = new Exception("Guild является null")
                    });

                foreach(SocketGuildUser user in guild.Users)
                    GuildUsers.TryAdd(user.Id, user);

                return Task.FromResult(new DefaultResponse() 
                { 
                    IsSuccess = true, 
                    Message = "Кэш пользователей успешно заполнен"
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при заполнении кэша пользователей");

                return Task.FromResult(new DefaultResponse()
                {
                    IsSuccess = false,
                    Message = "Ошибка при заполнении кэша пользователей",
                    Exception = ex
                });
            }
        }
        public ConcurrentDictionary<ulong, SocketGuildUser> GetAllUsers() => GuildUsers;
    }
}
