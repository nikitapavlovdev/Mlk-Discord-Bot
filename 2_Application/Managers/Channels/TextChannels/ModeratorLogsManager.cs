using Discord.WebSocket;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._2_Application.DTOs.Messages;
using MlkAdmin._3_Infrastructure.Discord.Extensions;

namespace MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers
{
    public class ModeratorLogsManager(
        DiscordSocketClient discordSocketClient,
        EmbedMessageExtension embedMessageExtension) : IModeratorLogsSender
    {
        public async Task SendLogMessageAsync(LogMessageDto logMessageDto)
        {
            SocketGuild guild = discordSocketClient.GetGuild(logMessageDto.GuildId);
            SocketTextChannel? logsChannel = guild.TextChannels.FirstOrDefault(x => x.Id == logMessageDto.ChannelId);

            await logsChannel.SendMessageAsync(embed: embedMessageExtension.CreateEmbed(new()
            {
                Title = logMessageDto.Title,
                Description = logMessageDto.Description,
            }));
        }
    }
}
