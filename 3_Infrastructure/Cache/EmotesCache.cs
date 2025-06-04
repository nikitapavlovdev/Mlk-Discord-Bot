using Discord;

namespace MlkAdmin.Infrastructure.Cache
{
    public class EmotesCache
    {
        private readonly Dictionary<ulong, GuildEmote> MainServerEmotes = [];
        private IReadOnlyCollection<GuildEmote>? GuildEmotes;
        public GuildEmote? GetEmote(ulong emoteId)
        {
            if (MainServerEmotes.TryGetValue(emoteId, out GuildEmote? emote))
            {
                return emote;
            }

            return null;
        }
        public GuildEmote? GetEmote(string name)
        {
            return GuildEmotes.First(x => x.Name == name);
        }
        public void AddEmote(GuildEmote emote)
        {
            MainServerEmotes.TryAdd(emote.Id, emote);
        }
        public async Task GuildEmotesInitialization(IReadOnlyCollection<GuildEmote> guildEmotes)
        {
            GuildEmotes = guildEmotes;
            await Task.CompletedTask;
        }
    }
}
