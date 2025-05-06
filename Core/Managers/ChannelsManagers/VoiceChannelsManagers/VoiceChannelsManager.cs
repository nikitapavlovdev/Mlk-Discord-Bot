using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;
using Discord_Bot.Core.Providers.JsonProvider;
using Discord_Bot.Infrastructure.Cache;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Core.Managers.ChannelsManagers.VoiceChannelsManagers
{
    public class VoiceChannelsManager(
        ChannelsCache channelsCache,
        ILogger<VoiceChannelsManager> logger,
        JsonDiscordCategoriesProvider jsonDiscordCategoriesProvider,
        JsonChannelsMapProvider jsonChannelsMapProvider, 
        ModeratorLogsSender moderatorLogsSender)
    {
        private readonly string[] memNames =
            ["blackman hurtz", "anx negropodobniy", "kroshka mrpronin",
            "lobachok", "lev esli i sosal..", "volosatiy yeban", "blackbeer party",
            "anx sosal"];
        private string GetLobbyName()
        {
            Random random = new();

            return memNames[random.Next(memNames.Length)];
        }
        private string GetLobbyName(SocketGuildUser socketGuildUser)
        {
            if (socketGuildUser.DisplayName.Length < 15)
            {
                return socketGuildUser.DisplayName;
            }

            return GetLobbyName();
        }
        public async Task GuildVoiceChannelsInitialization(SocketGuild socketGuild)
        {
            try
            {
                await LoadVoiceChannelsFromGuild(socketGuild);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}", ex.Message);
            }
        }
        public async Task ClearTemporaryVoiceChannels(SocketGuild socketGuild)
        {
            foreach(SocketVoiceChannel socketVoiceChannel in socketGuild.VoiceChannels)
            {
                if(socketVoiceChannel.Category.Id == jsonDiscordCategoriesProvider.RootDiscordCategories.Guild.Autolobby.Id 
                    && socketVoiceChannel.Id != jsonChannelsMapProvider.RootChannel.Channels.VoiceChannels.AutoLobby.AutoGamesLobby.Id)
                {
                    if(socketVoiceChannel.ConnectedUsers.Count == 0)
                    {
                        await moderatorLogsSender.SendRemovingVoiceChannelMessage(socketVoiceChannel, socketGuild, "VoiceChannelsManager", "ClearTemporaryVoiceChannels");
                        await socketVoiceChannel.DeleteAsync();
                    }
                    else
                    {
                        channelsCache.AddTemporaryChannel(socketVoiceChannel);
                    }
                }
            }
        }
        public async Task<RestVoiceChannel> CreateVoiceChannelAsync(SocketGuild socketGuild, SocketUser socketUser)
        {
            SocketGuildUser? leader = socketUser as SocketGuildUser;

            return await socketGuild.CreateVoiceChannelAsync(
                $"🔉 | {GetLobbyName(leader)}",
                properties =>
                {
                    properties.CategoryId = jsonDiscordCategoriesProvider.RootDiscordCategories.Guild.Autolobby.Id;
                    properties.Bitrate = 64000;
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
        private async Task LoadVoiceChannelsFromGuild(SocketGuild socketGuild)
        {
            foreach (SocketVoiceChannel socketVoiceChannel in socketGuild.VoiceChannels)
            {
                channelsCache.AddVoiceChannel(socketVoiceChannel);
            }

            await Task.CompletedTask;
        }
    }
}
