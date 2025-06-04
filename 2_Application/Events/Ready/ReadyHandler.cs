using Discord.WebSocket;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using MediatR;
using MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers;

namespace MlkAdmin._2_Application.Notifications.Ready
{
    public class ReadyHandler(
        TextMessageManager textMessageManager,
        VoiceChannelsManager voiceChannelsManager,
        DiscordSocketClient client,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider) : INotificationHandler<Ready>
    {
        public async Task Handle(Ready notification, CancellationToken cancellationToken)
        {
            SocketGuild? Guild = client.Guilds.FirstOrDefault(x => x.Id == jsonDiscordConfigurationProvider.RootDiscordConfiguration.Guild.Id);

            await Task.WhenAll(
                textMessageManager.SendDynamicMessages(Guild),
                voiceChannelsManager.ClearTemporaryVoiceChannels(Guild)
            );
        }
    }
}
