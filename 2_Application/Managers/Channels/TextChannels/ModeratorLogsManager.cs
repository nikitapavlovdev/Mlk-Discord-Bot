using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._2_Application.DTOs;
using Discord.WebSocket;

namespace MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers
{
    public class ModeratorLogsManager(DiscordSocketClient discordSocketClient) : IModeratorLogsSender
    {
        public async Task SendLogMessageAsync(LogMessageDto logMessageDto)
        {
            SocketGuild guild = discordSocketClient.GetGuild(logMessageDto.GuildId);
            SocketTextChannel? logsChannel = guild.TextChannels.FirstOrDefault(x => x.Id == logMessageDto.ChannelId);

            await logsChannel.SendMessageAsync(embed: EmbedMessageExtension.GetDefaultEmbedTemplate(logMessageDto.Title, logMessageDto.Description));
        }
    }
}
