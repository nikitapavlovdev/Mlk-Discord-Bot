using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._3_Infrastructure.DataBase;

namespace MlkAdmin._2_Application.Managers.Channels.VoiceChannels
{
    public class VoiceChannelSyncServices(ILogger<TextChannelsRepository> logger, 
        MlkAdminDbContext mlkAdminDbContext)
    {
        public async Task SyncVoiceChannelsDbWithGuildAsync(SocketGuild socketGuild)
        {
            try
            {
                List<VoiceChannel> dbVoiceChannels = await mlkAdminDbContext.Voices.ToListAsync();
                HashSet<ulong> existingChannels = [.. socketGuild.VoiceChannels.Select(x => x.Id)];
                List<VoiceChannel> toRemoveChannels = [.. dbVoiceChannels.Where(x => !existingChannels.Contains(x.Id))];

                mlkAdminDbContext.Voices.RemoveRange(toRemoveChannels);
                await mlkAdminDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
