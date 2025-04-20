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
        public async Task GuildRolesInitialization(SocketGuild socketGuild)
        {
            try
            {
                await LoadRolesFromGuild(socketGuild);

            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }

        public async Task AddNotRegisteredRoleAsync(SocketGuildUser socketGuildUser)
        {
            try
            {
                await socketGuildUser.AddRoleAsync(rolesCache.GetRole(
                    jsonDiscordRolesProvider
                    .RootDiscordRoles
                    .GeneralRole
                    .Autorization
                    .NotRegistered
                    .Id));
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
                await socketGuildUser.RemoveRoleAsync(rolesCache.GetRole(
                    jsonDiscordRolesProvider
                    .RootDiscordRoles
                    .GeneralRole
                    .Autorization
                    .NotRegistered
                    .Id));
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
                await socketGuildUser.AddRoleAsync(rolesCache.GetRole(
                    jsonDiscordRolesProvider
                    .RootDiscordRoles
                    .GeneralRole
                    .Autorization
                    .MalenkiyMember
                    .Id));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public async Task DeleteSelectionRoles(SocketGuildUser socketGuildUser)
        {
            Dictionary<ulong, SocketRole> AvailableForSelectionRoles = rolesCache.GetDictionarySelectionRoles();

            foreach (SocketRole role in AvailableForSelectionRoles.Values)
            {
                if (socketGuildUser.Roles.Any(userRole => userRole == role))
                {
                    await socketGuildUser.RemoveRoleAsync(role);
                }
            }
        }

        private async Task LoadRolesFromGuild(SocketGuild socketGuild)
        {
            foreach (SocketRole socketRole in socketGuild.Roles)
            {
                rolesCache.AddRole(socketRole);
            }

            await Task.CompletedTask;
        }

    }
}
