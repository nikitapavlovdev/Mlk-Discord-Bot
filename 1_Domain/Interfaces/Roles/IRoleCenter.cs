using Discord.WebSocket;

namespace MlkAdmin._1_Domain.Interfaces.Roles
{
    public interface IRoleCenter
    {
        public Task AddRoleToUserAsync(SocketGuildUser user, ulong roleId);
        public Task AddRolesToUserAsync(SocketGuildUser user, ulong[] roleIds);
        public Task RemoveRoleFromUserAsync(SocketGuildUser user, ulong roleId);
    }
}
