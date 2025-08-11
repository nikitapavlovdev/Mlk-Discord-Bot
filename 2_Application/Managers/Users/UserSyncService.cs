using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces;

namespace MlkAdmin._2_Application.Managers.Users
{
    public class UserSyncService(ILogger<UserSyncService> logger, IUserRepository userRepository)
    {
        public async Task SyncUsersAsync(SocketGuild guild)
        {
            try
            {
                foreach (var user in guild.Users)
                {
                    User dtoUser = new()
                    {
                        Id = user.Id,
                        DiscordDisplayName = user.DisplayName,
                        DiscordGlobalName = user.GlobalName,
                        GuildId = guild.Id,
                        GuildJoinedAt = DateTime.UtcNow
                    };

                    await userRepository.UpsertUserAsync(dtoUser);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
