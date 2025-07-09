using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin._3_Infrastructure.DataBase;

namespace MlkAdmin._2_Application.Managers.Channels.VoiceChannels
{
    public class VoiceChannelRepository(
        ILogger<VoiceChannelRepository> logger,
        MlkAdminDbContext mlkAdminDbContext) : IVoiceChannelRepository
    {
        public async Task UpsertDbVoiceChannelAsync(VoiceChannel channel)
        {
            try
            {
                VoiceChannel? dbChannel = await mlkAdminDbContext.Voices.FirstOrDefaultAsync(x => x.Id == channel.Id);

                if (dbChannel == null)
                {
                    await mlkAdminDbContext.Voices.AddAsync(channel);
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

        public async Task RemoveDbVoiceChannelAsync(ulong id)
        {
            try
            {
                VoiceChannel? dbChannel = await mlkAdminDbContext.Voices.FirstOrDefaultAsync(x => x.Id == id);

                if (dbChannel == null) { return; }

                mlkAdminDbContext.Voices.Remove(dbChannel);
                await mlkAdminDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }

        public async Task<bool> IsTemporaryVoiceChannel(ulong id)
        {
            VoiceChannel? channel = await mlkAdminDbContext.Voices.FirstOrDefaultAsync(x => x.Id == id);

            if (channel == null) { return false; };

            return channel.IsTemporary;
            
        }
        public async Task<bool> IsGeneratingVoiceChannel(ulong id)
        {
            VoiceChannel? channel = await mlkAdminDbContext.Voices.FirstOrDefaultAsync(x => x.Id == id);

            if (channel == null) { return false; }
            ;

            return channel.IsGenerating;
        }
    }
}