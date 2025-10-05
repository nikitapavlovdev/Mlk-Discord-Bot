using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Roles;

namespace MlkAdmin._2_Application.Services.Roles
{
    public class RolesService(ILogger<RolesService> logger) : IRoleCenter
    {
        public async Task AddRoleToUserAsync(SocketGuildUser user, ulong roleId)
        {
            if(user.Roles.Any(x => x.Id == roleId))
            {
                return;
            }

            await user.AddRoleAsync(roleId);
        }
        public async Task AddRolesToUserAsync(SocketGuildUser user, ulong[] roleIds)
        {
            try
            {
                if (roleIds.Length == 0)
                {
                    return;
                }

                foreach (ulong roleId in roleIds)
                {
                    if(user.Roles.Any(x => x.Id == roleId))
                    {
                        continue;
                    }

                    await user.AddRoleAsync(roleId);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("{Message}", ex.Message);
            }
        }
        public async Task RemoveRoleFromUserAsync(SocketGuildUser user, ulong roleId)
        {
            try
            {
                if(!user.Roles.Any(role => role.Id == roleId))
                {
                    return;
                }

                await user.RemoveRoleAsync(roleId);
            }
            catch (Exception ex)
            {
                logger.LogError("{ex}", ex.Message);
            }
        }
    }
}
