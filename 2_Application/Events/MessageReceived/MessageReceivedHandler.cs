using MediatR;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin._1_Domain.Entities;
using Discord;

namespace MlkAdmin._2_Application.Events.MessageReceived
{
    public class MessageReceivedHandler(
        ILogger<MessageReceivedHandler> logger,
        IUserRepository userRepository) : INotificationHandler<MessageReceived>
    {
        public async Task Handle(MessageReceived notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.SocketMessage.Author.IsBot) return;
                if (notification.SocketMessage is not SocketUserMessage socketUserMessage) return;

                SocketGuildUser? socketGuildUser = notification.SocketMessage.Author as SocketGuildUser;

                int argPos = 0;

                if (socketGuildUser.Id == 949714170453053450 && socketUserMessage.HasStringPrefix("$mlk:", ref argPos))
                {
                    string[] command = notification.SocketMessage.Content[argPos..].Split(" ");
                    string title = "ᴍᴀʟᴇɴᴋɪᴇ 🠒 ᴄᴏᴍᴍᴀɴᴅ";

                    switch (command[0])
                    {
                        case "rm":

                            if (socketUserMessage.Channel is not ITextChannel textChannel)
                            {
                                return;
                            }

                            IEnumerable<IMessage> messages = await textChannel.GetMessagesAsync(limit: 100).FlattenAsync();
                            await textChannel.DeleteMessagesAsync(messages);

                            break;

                        case "lobbyname":

                            string lobbyName = "";

                            for(int i = 1; i < command.Length; i++)
                            {
                                lobbyName += " " + command[i];
                            }

                            User? user = await userRepository.GetDbUserAsync(notification.SocketMessage.Author.Id);
                            user.LobbyName = lobbyName;

                            await userRepository.UpsertUserAsync(user);

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
