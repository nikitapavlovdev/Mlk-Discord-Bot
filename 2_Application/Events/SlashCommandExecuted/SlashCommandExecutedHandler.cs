using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Commands;

namespace MlkAdmin._2_Application.Events.SlashCommandExecuted
{
    public class SlashCommandExecutedHandler(
		ILogger<SlashCommandExecutedHandler> logger,
		IMediator mediator) : INotificationHandler<SlashCommandExecuted>
    {
        public async Task Handle(SlashCommandExecuted notification, CancellationToken cancellationToken)
        {
			try
			{
				switch (notification.SocketSlashCommand.CommandName)
				{
					case "set_lobby_name":
						await mediator.Send(new LobbyNameCommand()
						{
							LobbyName = notification.SocketSlashCommand.Data.Options.FirstOrDefault(x => x.Name == "lobbyname").Value.ToString() ?? string.Empty,
							UserId = notification.SocketSlashCommand.User.Id
						}, new());

                        await notification.SocketSlashCommand.RespondAsync("Paff, балуюсь листьями!", ephemeral: true);

                        break;

					default:
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
