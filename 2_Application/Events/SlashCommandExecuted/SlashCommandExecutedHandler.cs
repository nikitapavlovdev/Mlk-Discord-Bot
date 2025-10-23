using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Commands.LobbyName;
using MlkAdmin._2_Application.Commands.UserStat;
using MlkAdmin._2_Application.DTOs.Discord.Embed;
using MlkAdmin._2_Application.DTOs.Discord.Responses;
using MlkAdmin._2_Application.DTOs.Responses;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using System.Net.Http.Headers;

namespace MlkAdmin._2_Application.Events.SlashCommandExecuted
{
    public class SlashCommandExecutedHandler(
		ILogger<SlashCommandExecutedHandler> logger,
		IMediator mediator, 
		JsonDiscordChannelsMapProvider mapProvider,
        EmbedMessageExtension embedMessageExtension) : INotificationHandler<SlashCommandExecuted>
    {
        public async Task Handle(SlashCommandExecuted notification, CancellationToken cancellationToken)
        {
			try
			{
				if(notification.SocketSlashCommand.Channel.Id != mapProvider.BotCommandChannelId)
				{
					await notification.SocketSlashCommand.RespondAsync(
						embed: embedMessageExtension.CreateEmbed(new EmbedDto() 

                        {
							Title = "Кок-блок",
                            Description = $"Команды бота можно вызывать только в канале {mapProvider.BotCommandChannelHttps}.",
                            Color = Discord.Color.Red
                        }),
						ephemeral: true);

					return;
				}

				switch (notification.SocketSlashCommand.CommandName)
				{
					case "set_lobby":

						LobbyNameResponse lobbyNameResponse = await mediator.Send(new LobbyNameCommand()
						{
							LobbyName = notification.SocketSlashCommand.Data.Options.FirstOrDefault(x => x.Name == "name").Value.ToString() ?? string.Empty,
							UserId = notification.SocketSlashCommand.User.Id
						}, new());

                        await notification.SocketSlashCommand.RespondAsync(
							embed: embedMessageExtension.CreateEmbed(new EmbedDto()
							{
								Description = lobbyNameResponse.Message,
                                Color = lobbyNameResponse.IsSuccess ? Discord.Color.Green : Discord.Color.Red
                            }), 
							ephemeral: false);
                        break;

					case "statistic":

						UserStatResponse userStatResponse = await mediator.Send(new UserStatCommand()
						{
							UserId = notification.SocketSlashCommand.User.Id
						}, new());

						await notification.SocketSlashCommand.RespondAsync(embed: embedMessageExtension.CreateEmbed(new EmbedDto()
						{
							Description = $"> Отправленных сообщений: {userStatResponse.MessageCount}\n" +
							$"> Времени в голосовом канале: **{userStatResponse.TotalSeconds}**",
							Title = $"Статистика по пользователю: **{notification.SocketSlashCommand.User.Mention}**"
						}));

						break;
					default:
                        await notification.SocketSlashCommand.RespondAsync(
							embed: embedMessageExtension.CreateEmbed(new EmbedDto()
							{
								Title = "Технические шоколадки",
								Description = "Команда пока что в разработке",
                                Color = Discord.Color.Default
                            }),
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
