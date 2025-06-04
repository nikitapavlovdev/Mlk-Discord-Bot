using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin.Infrastructure.Cache;

namespace MlkAdmin.Application.Managers.EmotesManagers
{
    public class EmotesManager(
        ILogger<EmotesManager> logger,
        EmotesCache emotesCache)
    {
        public async Task EmotesInitialization(SocketGuild socketGuild)
        {
            await LoadEmotesFromGuild(socketGuild);
            await CloneEmotesFromGuild(socketGuild);
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
        private async Task CloneEmotesFromGuild(SocketGuild socketGuild)
        {
           await emotesCache.GuildEmotesInitialization(socketGuild.Emotes);
        }
    }
}
