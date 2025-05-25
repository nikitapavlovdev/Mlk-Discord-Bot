using Discord;
using Discord.Rest;
using Discord.WebSocket;
using MlkAdmin.Core.Managers.ChannelsManagers.TextChannelsManagers;
using MlkAdmin.Core.Providers.JsonProvider;
using MlkAdmin.Infrastructure.Cache;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Managers.UserManagers;

namespace MlkAdmin.Core.Managers.ChannelsManagers.VoiceChannelsManagers
{
    public class VoiceChannelsManager(
        ChannelsCache channelsCache,
        ILogger<VoiceChannelsManager> logger,
        JsonDiscordCategoriesProvider jsonDiscordCategoriesProvider,
        JsonChannelsMapProvider jsonChannelsMapProvider, 
        ModeratorLogsSender moderatorLogsSender,
        StaticDataServices staticDataServices)
    {

        private string GetLobbyName(ulong userId)
        {
            Random rnd = new();
            string uniqueName = staticDataServices.GetUniqueLobbyName(userId);

            if(rnd.Next(0, 1000000) == 0)
            {
                return "🤍 Million Amnymchik Kid";
            }

            if (rnd.Next(0, 100000) == 0)
            {
                return "💖 One Hundred Thousand Kid";
            }

            if (rnd.Next(0, 1000) == 0)
            {
                return "💜 One Thousand Kid";
            }

            if(uniqueName != string.Empty)
            {
                return uniqueName;
            }

            return "ᴍʟᴋ_ʟᴏʙʙʏ";
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
                $"🔉 | {GetLobbyName(socketUser.Id)}",
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
