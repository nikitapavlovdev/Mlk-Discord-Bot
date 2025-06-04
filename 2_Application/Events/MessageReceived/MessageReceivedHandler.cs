using MediatR;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Utilities.DI;
using Discord;

namespace MlkAdmin.Application.Notifications.MessageReceived
{
    public class MessageReceivedHandler(
        ILogger<MessageReceivedHandler> logger) : INotificationHandler<MessageReceived>
    {
        public async Task Handle(MessageReceived notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.SocketMessage.Author.IsBot) return;
                if (notification.SocketMessage is not SocketUserMessage socketUserMessage) return;

                SocketGuildUser? socketGuildUser = notification.SocketMessage.Author as SocketGuildUser;

                int argPos = 0;

                if (socketGuildUser.Id == 628236760681545748 && socketUserMessage.HasStringPrefix("$mlk:", ref argPos))
                {
                    string command = notification.SocketMessage.Content[argPos..];
                    string title = "ᴍᴀʟᴇɴᴋɪᴇ 🠒 ᴄᴏᴍᴍᴀɴᴅ";

                    switch (command)
                    {
                        case "rm":

                            if (socketUserMessage.Channel is not ITextChannel textChannel)
                            {
                                return;
                            }

                            IEnumerable<IMessage> messages = await textChannel.GetMessagesAsync(limit: 100).FlattenAsync();
                            await textChannel.DeleteMessagesAsync(messages);

                            break;


                        default:

                            await socketUserMessage.Channel.SendMessageAsync(
                                embed: EmbedMessageExtension.GetDefaultEmbedTemplate(title, "> Unknown command"));

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
