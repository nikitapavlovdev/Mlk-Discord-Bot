using Discord.WebSocket;
using Discord_Bot.Core.Utilities.General;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;

namespace Discord_Bot.Core.Managers.UserManagers
{
    public class PersonalDataManager(ILogger<PersonalDataManager> _logger)
    {
        public async Task GetUserPersonalData(SocketModal modal)
        {
            try
            {
                DateTime date = new();

                string name = modal.Data.Components.First(x => x.CustomId == "personal_data_input_name").Value;
                string bday = modal.Data.Components.First(x => x.CustomId == "personal_data_input_dateofbirthday").Value;
                string country = modal.Data.Components.First(x => x.CustomId == "personal_data_input_country").Value;
                string telegram = modal.Data.Components.First(x => x.CustomId == "personal_data_input_telegram").Value;

                if (!ExtensionMethods.DateOfBirthdayIsCorrect(bday, out date))
                {
                    await TextMessageSender.SendFollowupMessageOnSuccesInputPersonalData(modal);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
