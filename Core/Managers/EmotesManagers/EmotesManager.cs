using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Discord_Bot.Infrastructure.Cache;

namespace Discord_Bot.Core.Managers.EmotesManagers
{
    public class EmotesManager(ILogger<EmotesManager> logger,
        EmotesCache emotesCache)
    {
        public async Task EmotesInitialization(SocketGuild socketGuild)
        {
            await LoadEmotesFromGuild(socketGuild);
        }
        private async Task LoadEmotesFromGuild(SocketGuild socketGuild)
        {
            try
            {
                foreach (GuildEmote emote in socketGuild.Emotes)
                {
                    emotesCache.AddEmote(emote);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
