using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cash;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Utilities.General;

namespace Discord_Bot.Core.Managers.RolesManagers
{
    public class RolesManager(ILogger<RolesManager> _logger, IConfiguration _configuration, RolesCash _rolesCash)
    {
        private readonly SocketRole _notRegisteredRole = _rolesCash.GetRole(ExtensionMethods.ConvertId(_configuration["Roles:NotRegistered:Id"]));
        private readonly SocketRole _baseServerRole = _rolesCash.GetRole(ExtensionMethods.ConvertId(_configuration["Roles:MalenkiyMember:Id"]));
        public async Task AddNotRegisteredRoleAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                await socketGuildUser.AddRoleAsync(_notRegisteredRole);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
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
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
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
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
