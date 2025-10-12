using MediatR;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._1_Domain.Interfaces;
using Discord;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Events.MessageReceived
{
    public class MessageReceivedHandler(
        ILogger<MessageReceivedHandler> logger,
        JsonDiscordRolesProvider jsonDiscordRolesProvider) : INotificationHandler<MessageReceived>
    {
        public async Task Handle(MessageReceived notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.SocketMessage.Author.IsBot) return;
                if (notification.SocketMessage is not SocketUserMessage socketUserMessage) return;

                SocketGuildUser? socketGuildUser = notification.SocketMessage.Author as SocketGuildUser;

                int argPos = 0;

                if (socketGuildUser.Roles.Any(x => x.Id == jsonDiscordRolesProvider.AdminRoleId) || 
                    socketGuildUser.Roles.Any(x => x.Id == jsonDiscordRolesProvider.HeadRoleId))
                {
                    if (!socketUserMessage.HasStringPrefix("$adm:", ref argPos)) return;

                    string[] command = notification.SocketMessage.Content[argPos..].Split(" ");

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

                        default:

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
