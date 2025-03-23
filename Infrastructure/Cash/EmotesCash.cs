using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Infrastructure.Cash
{
    public class EmotesCash(ILogger<EmotesCash> _logger)
    {
        private readonly Dictionary<ulong, GuildEmote> MainServerEmotes = [];

        public async Task EmotesInitialization(SocketGuild socketGuild)
        {
            try
            {
                IReadOnlyCollection<GuildEmote> _emotes = socketGuild.Emotes;

                foreach (GuildEmote emote in _emotes)
                {
                    MainServerEmotes.TryAdd(emote.Id, emote);
                }

                _logger.LogInformation("Fill emotes status: Done");

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public GuildEmote GetEmote(ulong emoteId)
        {
            if (MainServerEmotes.TryGetValue(emoteId, out GuildEmote emote))
            {
                return emote;
            }

            _logger.LogWarning("Смайлик с ID {EmoteId} не найден", emoteId);
            return null;
        }
    }
}
