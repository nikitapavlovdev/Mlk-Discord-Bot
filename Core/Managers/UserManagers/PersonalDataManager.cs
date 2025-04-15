using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Core.Managers.UserManagers
{
    public class PersonalDataManager(ILogger<PersonalDataManager> _logger)
    {
        public async Task GetUserPersonalData(SocketModal modal)
        {
            try
            {
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
