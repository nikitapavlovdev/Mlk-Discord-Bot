using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cache;

namespace Discord_Bot.Core.Managers.ChannelsManagers.VoiceChannelsManagers
{
    public class VoiceChannelsCreator(ChannelsCache channelsCache)
    {
        public async Task<RestVoiceChannel> CreateVoiceChannelAsync(SocketGuild socketGuild, SocketVoiceChannel socketVoiceChannel, SocketUser leader)
        {
            return await socketGuild.CreateVoiceChannelAsync(
                $"🎵 | ᴍʟᴋʟᴏʙʙʏ {channelsCache.GetLobbyNumber()}",
                properties =>
                {
                    properties.CategoryId = socketVoiceChannel.CategoryId;
                    properties.Bitrate = 96000;
                    properties.RTCRegion = "rotterdam";
                    properties.PermissionOverwrites = new Overwrite[]
                    {
                        new(
                            socketGuild.EveryoneRole.Id,
                            PermissionTarget.Role,
                            new OverwritePermissions(connect: PermValue.Allow, sendMessages: PermValue.Allow, manageChannel: PermValue.Deny)
                        ),

                        new(
                            leader.Id,
                            PermissionTarget.User,
                            new OverwritePermissions(connect: PermValue.Allow, sendMessages: PermValue.Allow, manageChannel: PermValue.Allow)
                        )
                    };
                }
            );
        }
    }
}
