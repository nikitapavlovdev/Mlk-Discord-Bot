using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Roles;
using MlkAdmin._2_Application.DTOs.Responses;
using MlkAdmin._2_Application.Services.Users;

namespace MlkAdmin._2_Application.Services.Roles
{
    public class RolesService(
        ILogger<RolesService> logger,
        UserService userService) : IRoleCenter
    {
        public async Task<DefaultResponse> AddRoleToUserAsync(ulong userId, ulong roleId)
        {
            try
            {
                SocketGuildUser user = userService.GetGuildUser(userId) ?? throw new Exception("Пользователь не найден");
                
                if (user.Roles.Any(x => x.Id == roleId))
                {
                    return new DefaultResponse()
                    {
                        IsSuccess = false,
                        Message = "Роль уже назначена"
                    };
                }

                await user.AddRoleAsync(roleId);

                return new DefaultResponse()
                {
                    IsSuccess = true,
                    Message = "Роль успешно назначена"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке назначить роль");

                return new DefaultResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
        public async Task<DefaultResponse> AddRolesToUserAsync(ulong userId, ulong[] roleIds)
        {
            try
            {

                if (roleIds.Length == 0)
                {
                    return new DefaultResponse()
                    {
                        IsSuccess = false,
                        Message = "Список ролей пуст"
                    };
                }

                string responseMessage = string.Empty;
                
                SocketGuildUser user = userService.GetGuildUser(userId) ?? throw new Exception("Пользователь не найден");

                
                foreach (ulong roleId in roleIds)
                {
                    if(user.Roles.Any(x => x.Id == roleId))
                    {
                        responseMessage += $"Роль {user.Roles.First(x=> x.Id == roleId).Mention} уже была присвоена";
                        continue;
                    }

                    await user.AddRoleAsync(roleId);
                }

                return new DefaultResponse()
                {
                    IsSuccess = true,
                    Message = responseMessage,
                };
            }
            catch (Exception ex)
            {
                logger.LogError("{Message}", ex.Message);

                return new DefaultResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
        public async Task<DefaultResponse> RemoveRoleFromUserAsync(ulong userId, ulong roleId)
        {
            try
            {
                SocketGuildUser user = userService.GetGuildUser(userId) ?? throw new Exception("Пользователь не найден");

                if (!user.Roles.Any(role => role.Id == roleId))
                {
                    return new DefaultResponse()
                    {
                        IsSuccess = false,
                        Message = "Удаляемой роли не найдено"
                    };
                }

                await user.RemoveRoleAsync(roleId);

                return new DefaultResponse()
                {
                    IsSuccess = true,
                    Message = "Роль успешно удалена"
                };
            }
            catch (Exception ex)
            {
                logger.LogError("{ex}", ex.Message);

                return new DefaultResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
        public async Task<DefaultResponse> RemoveRolesFromUserAsync(ulong userId, ulong[] roleIds)
        {
            try
            {
                if (roleIds.Length == 0)
                {
                    return new DefaultResponse()
                    {
                        IsSuccess = false,
                        Message = "Список ролей пуст"
                    };
                }

                SocketGuildUser user = userService.GetGuildUser(userId) ?? throw new Exception("Пользователь не найден");

                foreach (ulong roleId in roleIds)
                {
                    if(!user.Roles.Any(x => x.Id == roleId))
                    {
                        continue;
                    }

                    await user.RemoveRoleAsync(roleId);
                }

                return new DefaultResponse()
                {
                    IsSuccess = true,
                    Message = "Роли успешно отчищены"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке удалить роли");

                return new DefaultResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
    } 
}
