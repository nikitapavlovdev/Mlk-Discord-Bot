using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._3_Infrastructure.DataBase;

namespace MlkAdmin._2_Application.Managers.Channels.VoiceChannels
{
    public class TextChannelRepository(
        ILogger<TextChannelRepository> logger,
        MlkAdminDbContext mlkAdminDbContext) : ITextChannelRepository
    {
        public async Task UpsertDbTextChannelAsync(TextChannel channel)
        {
            try
            {
                TextChannel? dbChannel = await mlkAdminDbContext.Texts.FirstOrDefaultAsync(x => x.Id == channel.Id);

                if (dbChannel == null)
                {
                    await mlkAdminDbContext.Texts.AddAsync(channel);
                }
                else
                {
                    mlkAdminDbContext.Entry(dbChannel).CurrentValues.SetValues(channel);
                }

                await mlkAdminDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }

        public async Task RemoveDbTextChannelAsync(ulong id)
        {
            try
            {
                TextChannel? dbChannel = await mlkAdminDbContext.Texts.FirstOrDefaultAsync(x => x.Id == id);

                if (dbChannel == null) { return; }

                mlkAdminDbContext.Texts.Remove(dbChannel);
                await mlkAdminDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}