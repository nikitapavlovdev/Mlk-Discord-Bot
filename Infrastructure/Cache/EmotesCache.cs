using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace Discord_Bot.Infrastructure.Cache
{
    public class EmotesCache(ILogger<EmotesCache> logger)
    {
        private readonly Dictionary<ulong, GuildEmote> MainServerEmotes = [];

        public GuildEmote? GetEmote(ulong emoteId)
        {
            if (MainServerEmotes.TryGetValue(emoteId, out GuildEmote? emote))
            {
                return emote;
            }

            return null;
        }
        public void AddEmote(GuildEmote emote)
        {
            MainServerEmotes.TryAdd(emote.Id, emote);
        }
    }
}
