using Discord.WebSocket;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using MediatR;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Notifications.Ready
{
    public class ReadyHandler(
        ILogger<ReadyHandler> logger,
        IDynamicMessageCenter dynamicMessageCenter,
        VoiceChannelsManager voiceChannelsManager,
        DiscordSocketClient client,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider) : INotificationHandler<Ready>
    {
        public async Task Handle(Ready notification, CancellationToken cancellationToken)
        {
            try
            {
                SocketGuild? guild = client.Guilds.FirstOrDefault(x => x.Id == jsonDiscordConfigurationProvider.GuildId);

                await Task.WhenAll(
                    dynamicMessageCenter.UpdateAllDM(guild.Id),
                    voiceChannelsManager.ClearTemporaryVoiceChannels(guild)
                );
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
