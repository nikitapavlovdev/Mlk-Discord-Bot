using MediatR;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Utilities.DI;

namespace Discord_Bot.Core.Notifications.MessageReceived
{
    public class MessageReceivedNotificationHandler(
        ILogger<MessageReceivedNotificationHandler> logger) : INotificationHandler<MessageReceivedNotification>
    {
        public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.SocketMessage.Author.IsBot) return;
                if (notification.SocketMessage is not SocketUserMessage socketUserMessage) return;

                SocketGuildUser? socketGuildUser = notification.SocketMessage.Author as SocketGuildUser;

                int argPos = 0;

                if (socketGuildUser.Id == 628236760681545748 && socketUserMessage.HasStringPrefix("mlkbot:", ref argPos))
                {
                    string command = notification.SocketMessage.Content[argPos..];
                    string title = "ᴍᴀʟᴇɴᴋɪᴇ 🠒 ᴄᴏᴍᴍᴀɴᴅ";
                    
                    switch (command)
                    {
                        case "udm":

                            await socketUserMessage.Channel.SendMessageAsync(
                                embed: ExtensionEmbedMessage.GetDefaultEmbedTemplate(title, $"> Command **{command}** has been successful"));

                            await socketUserMessage.DeleteAsync();

                            break;

                        default:

                            await socketUserMessage.Channel.SendMessageAsync(
                                embed: ExtensionEmbedMessage.GetDefaultEmbedTemplate(title, "> Unknown command"));

                            await socketUserMessage.DeleteAsync();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
