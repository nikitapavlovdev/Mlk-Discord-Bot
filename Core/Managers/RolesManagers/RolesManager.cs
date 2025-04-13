using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cash;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Core.Managers.RolesManagers
{
    public class RolesManager(ILogger<RolesManager> logger, 
        RolesCash rolesCash,
        JsonDiscordRolesProvider jsonDiscordRolesProvider)
    {
        private readonly SocketRole _notRegisteredRole = rolesCash.GetRole(
            jsonDiscordRolesProvider
            .RootDiscordRoles
            .GeneralRole
            .Autorization
            .NotRegistered
            .Id);

        private readonly SocketRole _baseServerRole = rolesCash.GetRole(
            jsonDiscordRolesProvider
            .RootDiscordRoles
            .GeneralRole
            .Autorization
            .MalenkiyMember
            .Id);

        public async Task AddNotRegisteredRoleAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                await socketGuildUser.AddRoleAsync(_notRegisteredRole);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public async Task DeleteNotRegisteredRoleAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                await socketGuildUser.RemoveRoleAsync(_notRegisteredRole);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public async Task AddBaseServerRoleAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                await socketGuildUser.AddRoleAsync(_baseServerRole);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
