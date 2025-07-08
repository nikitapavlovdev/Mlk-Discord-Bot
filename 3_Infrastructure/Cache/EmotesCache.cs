using System.Data;
using System.Text.Json.Serialization;
using Discord;

namespace MlkAdmin.Infrastructure.Cache
{
    public class EmotesCache
    {
        private readonly Dictionary<ulong, GuildEmote> MainServerEmotes = [];
        private readonly TaskCompletionSource initTcs = new();

        private IReadOnlyCollection<GuildEmote>? GuildEmotes;
        
        public GuildEmote GetEmote(string name)
        {
            GuildEmote emote = GuildEmotes!.First(x => x.Name == name);
            return emote ?? throw new KeyNotFoundException($"Emote `{name}` not found.");
        }
        public void AddEmote(GuildEmote emote)
        {
            MainServerEmotes.TryAdd(emote.Id, emote);
        }

        public async Task CloneGuildEmotes(IReadOnlyCollection<GuildEmote> guildEmotes)
        {
            GuildEmotes = guildEmotes;
            await Task.CompletedTask;
        }
    }
}
