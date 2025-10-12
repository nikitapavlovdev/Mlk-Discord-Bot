using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.DTOs.Discord.Responses;
using System.Collections.Concurrent;

namespace MlkAdmin._3_Infrastructure.Cache.Users
{
    public class UsersCache(ILogger<UsersCache> logger)
    {
        private readonly ConcurrentDictionary<ulong, SocketGuildUser> GuildUsers = [];

        public Task<DefaultResponse> FillUsersAsync(SocketGuild guild)
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
        public Task<DefaultResponse> AddUserAsync(SocketGuildUser user)
        {
            try
            {
                if (user is null)
                    return Task.FromResult(new DefaultResponse()
                    {
                        IsSuccess = false,
                        Message = "Пользователь не найден",
                        Exception = new Exception("User является null")
                    });

                GuildUsers.TryAdd(user.Id, user);

                return Task.FromResult(new DefaultResponse()
                {
                    IsSuccess = true,
                    Message = "Пользователь успешно добавлен в кэш"
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при добавлении пользователя в кэш");
                return Task.FromResult(new DefaultResponse()
                {
                    IsSuccess = false,
                    Message = "Ошибка при добавлении пользователя в кэш",
                    Exception = ex
                });
            }
        }
        public ConcurrentDictionary<ulong, SocketGuildUser> GetAllUsers() => GuildUsers;
    }
}
