namespace MlkAdmin._1_Domain.Interfaces
{
    public interface IUserSyncService
    {
        public Task SyncUsersAsync(ulong guildId);
    }
}
