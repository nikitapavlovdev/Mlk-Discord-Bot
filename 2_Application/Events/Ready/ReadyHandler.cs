using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._4_Presentation.Discord;

namespace MlkAdmin._2_Application.Events.Ready
{
    public class ReadyHandler(
        ILogger<ReadyHandler> logger, 
        DiscordSlashCommandAdder discordSlashCommandAdder) : INotificationHandler<Ready>
    {
        public async Task Handle(Ready notification, CancellationToken cancellationToken)
        {
            try
            {
                await discordSlashCommandAdder.AddCommands();
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
