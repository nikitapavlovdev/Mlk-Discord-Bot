using MediatR;
using Discord.WebSocket;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.TextMessages;

namespace MlkAdmin._2_Application.Events.ButtonExecuted
{
    public class ButtonExecutedHandler(
        ILogger<ButtonExecutedHandler> logger,
        EmbedMessageExtension embedMessageExtension,
        IEmbedDtoCreator embedManager) : INotificationHandler<ButtonExecuted>
    {
        public async Task Handle(ButtonExecuted notification, CancellationToken cancellationToken)
        {
            try
            {
                if(notification.SocketMessageComponent.User is not SocketGuildUser socketGuildUser)
                {
                    return;
                }

                switch (notification.SocketMessageComponent.Data.CustomId)
                {
                    case "personal_data_button":
                        await notification.SocketMessageComponent.RespondWithModalAsync(ModalExtension.GetPersonalInformationModal());
                        return;

                    case "autolobby_naming_button":
                        await notification.SocketMessageComponent.RespondWithModalAsync(ModalExtension.GetLobbyNamingModal());
                        return;

                    case "feedback_button":
                        await notification.SocketMessageComponent.RespondWithModalAsync(ModalExtension.GetFeedBackModal());
                        return;

                    case "any_informations_button":
                        if (notification.SocketMessageComponent.User.Id == 949714170453053450)
                        {
                            await notification.SocketMessageComponent.RespondAsync(embed: embedMessageExtension.GetStaticMessageEmbedTamplate(await embedManager.GetEmbedDto(StaticMessageType.ServerPeculiarities)), ephemeral: true);
                        }
                        else
                        {
                            await notification.SocketMessageComponent.RespondAsync("Данный блок находится в разработке!", ephemeral: true);
                        }
                        return;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
