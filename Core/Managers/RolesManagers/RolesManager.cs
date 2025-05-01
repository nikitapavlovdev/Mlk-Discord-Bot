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
                await Task.WhenAll(
                    LoadRolesFromGuild(socketGuild),
                    LoadRolesDiscription()
                );
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
        private async Task LoadRolesFromGuild(SocketGuild socketGuild)
        {
            foreach (SocketRole socketRole in socketGuild.Roles)
            {
                rolesCache.AddRole(socketRole);
            }

            await Task.CompletedTask;
        }
        private async Task LoadRolesDiscription()
        {
            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.MalenkiyHead.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.MalenkiyHead.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.Moderator.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.Moderator.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.ServerBooster.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Hierarchy.ServerBooster.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.NotRegistered.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.NotRegistered.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.International.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.International.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.DeadInside.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.DeadInside.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Gus.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Gus.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Amnyam.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Amnyam.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Gacha.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Gacha.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Twitch.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Twitch.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.LadyFlora.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.LadyFlora.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.BlackBeer.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.BlackBeer.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Svin.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Unique.Svin.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.InformationHunter.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.IKIT.Description
            );

            rolesCache.AddRoleDescription(
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Gamer.Id,
                jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Gamer.Description
            );

            await Task.CompletedTask;
        }
    }
}
