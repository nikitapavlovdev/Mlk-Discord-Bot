using Discord;
using Discord.WebSocket;


namespace Discord_Bot.Core.Utilities.DI
{
    class ExtensionChannelsManager
    {
        public static async Task DeleteAllMessageFromChannel(IMessageChannel channel)
        {
            var messages = await channel.GetMessagesAsync().FlattenAsync();

            foreach (var message in messages)
            {
                message.DeleteAsync().Wait();
            }
        }
    }
}
