using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._2_Application.DTOs.Messages;
using MlkAdmin._3_Infrastructure.Discord.Extensions;

namespace MlkAdmin._2_Application.Services.Messages
{
    public class LogsService(
        ILogger<LogsService> logger,
        IChannelsService channelsService,
        EmbedMessageExtension embedMessageExtension) : IModeratorLogsSender
    {
        public async Task SendLogMessageAsync(LogMessageDto logMessageDto)
        {
            try
            {
                SocketTextChannel? logsChannel = await channelsService.GetTextChannelAsync(logMessageDto.ChannelId);

                await logsChannel.SendMessageAsync(embed: embedMessageExtension.CreateEmbed(new()
                {
                    Title = logMessageDto.Title,
                    Description = logMessageDto.Description,
                }));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при отправке лога");
            }
        }
    }
}
