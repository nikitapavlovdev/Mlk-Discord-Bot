using Discord.WebSocket;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using MediatR;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers;
using MlkAdmin._1_Domain.Interfaces.TextMessages;

namespace MlkAdmin._2_Application.Notifications.Ready
{
    public class ReadyHandler(
        IDynamicMessageCenter dynamicMessageCenter,
        VoiceChannelsManager voiceChannelsManager,
        DiscordSocketClient client,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider) : INotificationHandler<Ready>
    {
        public async Task Handle(Ready notification, CancellationToken cancellationToken)
        {
            SocketGuild? guild = client.Guilds.FirstOrDefault(x => x.Id == jsonDiscordConfigurationProvider.RootDiscordConfiguration.Guild.Id);

            await Task.WhenAll(
                dynamicMessageCenter.UpdateAllDM(guild.Id),
                voiceChannelsManager.ClearTemporaryVoiceChannels(guild)
            );
        }
    }
}
