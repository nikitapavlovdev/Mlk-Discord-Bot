using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cache;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Core.Managers.RolesManagers
{
    public class RolesManager(ILogger<RolesManager> logger, 
        RolesCache rolesCache,
        JsonDiscordRolesProvider jsonDiscordRolesProvider)
    {
        private readonly SocketRole _notRegisteredRole = rolesCache.GetRole(
            jsonDiscordRolesProvider
            .RootDiscordRoles
            .GeneralRole
            .Autorization
            .NotRegistered
            .Id);

        private readonly SocketRole _baseServerRole = rolesCache.GetRole(
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
