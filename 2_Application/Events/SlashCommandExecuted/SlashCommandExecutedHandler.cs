using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Commands;
using MlkAdmin._2_Application.DTOs;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Events.SlashCommandExecuted
{
    public class SlashCommandExecutedHandler(
		ILogger<SlashCommandExecutedHandler> logger,
		IMediator mediator, 
		JsonDiscordChannelsMapProvider mapProvider) : INotificationHandler<SlashCommandExecuted>
    {
        public async Task Handle(SlashCommandExecuted notification, CancellationToken cancellationToken)
        {
			try
			{
				if(notification.SocketSlashCommand.Channel.Id != mapProvider.BotCommandChannelId)
				{
					await notification.SocketSlashCommand.RespondAsync(
						embed: EmbedMessageExtension.GetDefaultEmbedTemplate(
							title: "Кок-блок",
							descriptions: $"Команды бота можно вызывать только в канале {mapProvider.BotCommandChannelHttps}.", 
							color: Discord.Color.Red),
						ephemeral: true);

					return;
				}

				switch (notification.SocketSlashCommand.CommandName)
				{
					case "set_lobby":

						LobbyNameResponse response = await mediator.Send(new LobbyNameCommand()
						{
							LobbyName = notification.SocketSlashCommand.Data.Options.FirstOrDefault(x => x.Name == "name").Value.ToString() ?? string.Empty,
							UserId = notification.SocketSlashCommand.User.Id
						}, new());

                        await notification.SocketSlashCommand.RespondAsync(
							embed: EmbedMessageExtension.GetDefaultEmbedTemplate(
								title: "", 
								descriptions: $"{response.Message}", 
								color: response.IsSuccess ? Discord.Color.Green : Discord.Color.Red), 
							ephemeral: false);
                        break;

					default:
                        await notification.SocketSlashCommand.RespondAsync(
							embed: EmbedMessageExtension.GetDefaultEmbedTemplate(
								title: "Технические шоколадки", 
								descriptions: "Команда пока что в разработке", 
								color: Discord.Color.Default),
							ephemeral: true);
                        break;
				}
			}
			catch (Exception ex)
			{
                logger.LogError("Ошибка в классе SlashCommandExecutedHandler: {ex}", ex.Message);
			}
        }
    }
}
