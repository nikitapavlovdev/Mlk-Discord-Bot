using Discord;
using Discord.WebSocket;
using Discord_Bot.Core.Providers.JsonProvider;
using Discord_Bot.Core.Utilities.DI;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Infrastructure.Cache
{
    public class ChannelsCache(
        ILogger<ChannelsCache> logger,
        ExtensionEmbedMessage embedMessage,
        ExtensionSelectionMenu selectionMenu,
        JsonChannelsMapProvider jsonChannelsMapProvider)
    {
        private readonly List<SocketVoiceChannel> AllVoiceChannels = [];
        private readonly List<ulong> GenereatingChannelsIds = [];
        private readonly List<ulong> TemporaryChannelIds = [];

        public async Task ChannelsInitialization(SocketGuild socketGuild)
        {
            try
            {
                await Task.WhenAll(
                    FillGeneratingChannels(socketGuild),
                    FillAllVoiceChannels(socketGuild)
                );

                await SendFormInRolesChannel(socketGuild);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}", ex.Message);
            }
        }

        private async Task FillAllVoiceChannels(SocketGuild socketGuild)
        {
            foreach(SocketVoiceChannel channel in socketGuild.VoiceChannels)
            {
                AllVoiceChannels.Add(channel);
            }

            await Task.CompletedTask;
        }
        private async Task FillGeneratingChannels(SocketGuild socketGuild)
        {
            foreach(SocketVoiceChannel channel in socketGuild.VoiceChannels)
            {
                if(channel.Id == jsonChannelsMapProvider.RootChannel.Channels.VoiceChannels.AutoLobby.AutoGamesLobby.Id)
                {
                    GenereatingChannelsIds.Add(jsonChannelsMapProvider.RootChannel.Channels.VoiceChannels.AutoLobby.AutoGamesLobby.Id);
                }
            }

            await Task.CompletedTask;
        }
        private async Task SendFormInRolesChannel(SocketGuild socketGuild)
        {
            SocketTextChannel? textChannel = socketGuild.TextChannels.FirstOrDefault(x => x.Id == (jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Id));
            MessageComponent component = selectionMenu.GetRolesSelectionMenu();

            if (textChannel == null)
            {
                return;
            }

            await ExtensionChannelsManager.DeleteAllMessageFromChannel(textChannel);
            await embedMessage.SendRolesMessage(textChannel, component);
        }
        public bool IsGeneratingChannel(ulong channelId)
        {
            return GenereatingChannelsIds.Any(x => x == channelId);
        }
        public bool IsTemporaryChannel(ulong channelId)
        {
            return TemporaryChannelIds.Any(x => x == channelId);
        }
        public void AddTemporaryChannelInList(IVoiceChannel channel)
        {
            TemporaryChannelIds.Add(channel.Id);
        }
        public void DeleteTemoraryChannelFromList(IVoiceChannel channel)
        {
            TemporaryChannelIds.Remove(channel.Id);
        }
        public int GetLobbyNumber()
        {
            return TemporaryChannelIds.Count;
        }
    }
}
