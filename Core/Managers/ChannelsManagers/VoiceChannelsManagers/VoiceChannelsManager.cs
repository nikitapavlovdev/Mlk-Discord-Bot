using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cache;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Core.Managers.ChannelsManagers.VoiceChannelsManagers
{
    public class VoiceChannelsManager(
        ChannelsCache channelsCache,
        ILogger<VoiceChannelsManager> logger)
    {
        private async Task LoadVoiceChannelsFromGuild(SocketGuild socketGuild)
        {
            foreach (SocketVoiceChannel socketVoiceChannel in socketGuild.VoiceChannels)
            {
                channelsCache.AddVoiceChannel(socketVoiceChannel);
            }

            await Task.CompletedTask;
        }
        public async Task GuildVoiceChannelsInitialization(SocketGuild socketGuild)
        {
            try
            {
                await LoadVoiceChannelsFromGuild(socketGuild);
                logger.LogInformation("Guild Voice Channels has been loaded");
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}", ex.Message);
            }
        }
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
