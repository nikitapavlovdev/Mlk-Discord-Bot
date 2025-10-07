using MlkAdmin._2_Application.DTOs.Responses;

namespace MlkAdmin._1_Domain.Interfaces.Roles
{
    public interface IRoleCenter
    {
        public Task<DefaultResponse> AddRoleToUserAsync(ulong userId, ulong roleId);
        public Task<DefaultResponse> AddRolesToUserAsync(ulong userId, ulong[] roleIds);
        public Task<DefaultResponse> RemoveRoleFromUserAsync(ulong userId, ulong roleId);
        public Task<DefaultResponse> RemoveRolesFromUserAsync(ulong userId, ulong[] roleIds);
    }
}