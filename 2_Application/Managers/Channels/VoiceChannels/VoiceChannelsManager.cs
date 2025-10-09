using Discord;
using Discord.Rest;
using Discord.WebSocket;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Users;
using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers
{
    public class VoiceChannelsManager(
        ILogger<VoiceChannelsManager> logger,
        IVoiceChannelRepository voiceChannelRepository,
        IUserRepository userRepository,
        JsonDiscordCategoriesProvider jsonDiscordCategoriesProvider,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider,
        JsonDiscordRolesProvider discordRolesProvider)
    {
        #region Private
        private async Task<string> GetLobbyName(ulong userId)
        {
            User? user = await userRepository.GetDbUserAsync(userId);

            if(user != null)
            {
                if (user.LobbyName != string.Empty && user.LobbyName != null)
                {
                    return user.LobbyName;
                }
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
                $"🔉 | {await GetLobbyName(socketUser.Id)}",
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