using Discord.WebSocket;
using MlkAdmin.Core.Managers.ChannelsManagers.TextChannelsManagers;
using MlkAdmin.Core.Managers.ChannelsManagers.VoiceChannelsManagers;
using MlkAdmin.Core.Providers.JsonProvider;
using MediatR;

namespace MlkAdmin.Core.Notifications.Ready
{
    public class ReadyNotificationHandler(
        TextMessageManager textMessageManager,
        VoiceChannelsManager voiceChannelsManager,
        DiscordSocketClient client,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider) : INotificationHandler<ReadyNotification>
    {
        public async Task Handle(ReadyNotification notification, CancellationToken cancellationToken)
        {
            SocketGuild? Guild = client.Guilds.FirstOrDefault(x => x.Id == jsonDiscordConfigurationProvider.RootDiscordConfiguration.Guild.Id);

            await Task.WhenAll(
                textMessageManager.SendDynamicMessages(Guild),
                voiceChannelsManager.ClearTemporaryVoiceChannels(Guild)
            );
        }
    }
}
