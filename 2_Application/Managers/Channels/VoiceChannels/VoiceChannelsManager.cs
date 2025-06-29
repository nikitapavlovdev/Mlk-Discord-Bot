using Discord;
using Discord.Rest;
using Discord.WebSocket;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using MlkAdmin.Infrastructure.Cache;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.UserManagers;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._2_Application.DTOs;

namespace MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers
{
    public class VoiceChannelsManager(
        ChannelsCache channelsCache,
        ILogger<VoiceChannelsManager> logger,
        IModeratorLogsSender moderatorLogsSender,
        JsonDiscordCategoriesProvider jsonDiscordCategoriesProvider,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider,
        JsonDiscordRolesProvider discordRolesProvider,
        StaticDataServices staticDataServices)
    {
        #region Controllers
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
        #endregion

        #region Private
        private async Task LoadVoiceChannelsFromGuild(SocketGuild socketGuild)
        {
            foreach (SocketVoiceChannel socketVoiceChannel in socketGuild.VoiceChannels)
            {
                channelsCache.AddVoiceChannel(socketVoiceChannel);
            }

            await Task.CompletedTask;
        }

        private string GetLobbyName(ulong userId)
        {
            Random rnd = new();
            string uniqueName = staticDataServices.GetUniqueLobbyName(userId);

            if (rnd.Next(0, 1000000) == 0)
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

            if (uniqueName != string.Empty)
            {
                return uniqueName;
            }

            return "ᴍʟᴋ_ʟᴏʙʙʏ";
        }

        #endregion

        #region Public
        public async Task ClearTemporaryVoiceChannels(SocketGuild socketGuild)
        {
            foreach (SocketVoiceChannel socketVoiceChannel in socketGuild.VoiceChannels)
            {
                if (socketVoiceChannel.Category.Id == jsonDiscordCategoriesProvider.AutoLobbyCategoryId
                    && socketVoiceChannel.Id != jsonChannelsMapProvider.AutoGameLobbyId)
                {
                    if (socketVoiceChannel.ConnectedUsers.Count == 0)
                    {
                        await moderatorLogsSender.SendLogMessageAsync(new LogMessageDto()
                        {
                            Description = "> Метод: ClearTemporaryVoiceChannels\n" +
                            $"> Удален канал: {socketVoiceChannel.Name}",
                            ChannelId = jsonChannelsMapProvider.LogsChannelId,
                            UserId = 0,
                            GuildId = socketVoiceChannel.Guild.Id,
                            Title = "Удаление канала"
                        });

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
                    properties.CategoryId = jsonDiscordCategoriesProvider.AutoLobbyCategoryId;
                    properties.Bitrate = 64000;
                    properties.PermissionOverwrites = new Overwrite[]
                    {
                        new(
                            discordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Gamer.Id,
                            PermissionTarget.Role,
                            new OverwritePermissions(
                                connect: PermValue.Allow,
                                sendMessages: PermValue.Allow,
                                manageChannel: PermValue.Deny)
                        ),
                        new(
                            leader.Id,
                            PermissionTarget.User,
                            new OverwritePermissions(manageChannel: PermValue.Allow)
                        )
                    };
                }
            );
        }
        #endregion
    }
}