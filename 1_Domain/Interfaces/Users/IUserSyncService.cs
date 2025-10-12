namespace MlkAdmin._1_Domain.Interfaces.Users
{
    public interface IUserSyncService
    {
        public Task SyncUsersAsync(ulong guildId);
    }
}
