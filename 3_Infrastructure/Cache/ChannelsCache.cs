using Discord.Rest;
using Discord.WebSocket;
using MlkAdmin.Infrastructure.Providers.JsonProvider;

namespace MlkAdmin.Infrastructure.Cache
{
    public class ChannelsCache(JsonChannelsMapProvider jsonChannelsMapProvider)
    {
        private readonly List<SocketVoiceChannel> GuildVoiceChannels = [];
        private readonly List<SocketTextChannel> GuildTextChannels = [];
        private readonly List<SocketVoiceChannel> GenereatingChannels = [];
        private readonly List<ulong> TemporaryVoiceChannels = [];

        public bool IsGeneratingChannel(SocketVoiceChannel socketVoiceChannel)
        {
            return GenereatingChannels.Any(x => x == socketVoiceChannel);
        }
        public bool IsTemporaryChannel(SocketVoiceChannel socketVoiceChannel)
        {
            return TemporaryVoiceChannels.Any(x => x == socketVoiceChannel.Id);
        }
        public void AddTemporaryChannel(RestVoiceChannel socketVoiceChannel)
        {
            if(!TemporaryVoiceChannels.Any(x => x == socketVoiceChannel.Id))
            {
                TemporaryVoiceChannels.Add(socketVoiceChannel.Id);
            }
        }
        public void AddTemporaryChannel(SocketVoiceChannel socketVoiceChannel)
        {
            TemporaryVoiceChannels.Add(socketVoiceChannel.Id);
        }
        public void AddVoiceChannel(SocketVoiceChannel socketVoiceChannel)
        {
            if(!GuildVoiceChannels.Any(x => x.Id == socketVoiceChannel.Id))
            {
                if (socketVoiceChannel.Id == jsonChannelsMapProvider.RootChannel.Channels.VoiceChannels.AutoLobby.AutoGamesLobby.Id)
                {
                    GenereatingChannels.Add(socketVoiceChannel);
                }

                GuildVoiceChannels.Add(socketVoiceChannel);
            } 
        }
        public void AddTextChannel(SocketTextChannel socketTextChannel)
        {
            if(!GuildTextChannels.Any(x => x.Id == socketTextChannel.Id))
            {
                GuildTextChannels.Add(socketTextChannel);
            }
        }
        public void DeleteTemporaryChannel(SocketVoiceChannel socketVoiceChannel)
        {
            foreach (ulong channelId in TemporaryVoiceChannels)
            {
                if (channelId == socketVoiceChannel.Id)
                {
                    TemporaryVoiceChannels.Remove(channelId);
                    return;
                }
            }
        }
    }
}
