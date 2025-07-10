using Discord;
using Discord.Rest;
using Discord.WebSocket;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.UserManagers;
using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers
{
    public class VoiceChannelsManager(
        ILogger<VoiceChannelsManager> logger,
        IVoiceChannelRepository voiceChannelRepository,
        JsonDiscordCategoriesProvider jsonDiscordCategoriesProvider,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider,
        JsonDiscordRolesProvider discordRolesProvider,
        StaticDataServices staticDataServices)
    {
        #region Private
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
        public async Task UpsertGuildVoiceChannelsAsync(SocketGuild socketGuild)
        {
            try
            {
                foreach (SocketVoiceChannel channel in socketGuild.VoiceChannels)
                {
                    VoiceChannel dbVoiceChannel = new()
                    {
                        Id = channel.Id,
                        ChannelName = channel.Name,
                        Category = channel.Category.ToString() ?? "No category",
                        IsGenerating = channel.Id == jsonChannelsMapProvider.AutoGameLobbyId,
                        IsTemporary = channel.Category.Id == jsonDiscordCategoriesProvider.AutoLobbyCategoryId && channel.Id != jsonChannelsMapProvider.AutoGameLobbyId
                    };

                    await voiceChannelRepository.UpsertDbVoiceChannelAsync(dbVoiceChannel);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
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