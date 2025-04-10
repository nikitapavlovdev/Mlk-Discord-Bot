using Discord.WebSocket;

namespace Discord_Bot.Core.Managers.ChannelsManagers.VoiceChannelsManagers
{
    public class VoiceChannelsCreator()
    {
        public static async Task CreateVoiceChannelAsync(SocketGuild socketGuild, ulong categoryId)
        {
            await socketGuild.CreateVoiceChannelAsync(
                "ᴍʟᴋʟᴏʙʙʏ: ",
                properties =>
                {
                    properties.CategoryId = categoryId;
                }
            );
        }
    }
}
