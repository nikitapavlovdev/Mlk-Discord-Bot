using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._1_Domain.Interfaces.Users
{
    public interface IUserRepository
    {
        Task UpsertUserAsync(User user);
        Task DeleteDbUserAsync(ulong id);
        Task<User?> GetDbUserAsync(ulong id);
    }
}
