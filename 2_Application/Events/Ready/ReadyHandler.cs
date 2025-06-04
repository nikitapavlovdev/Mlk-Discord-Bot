using Discord.WebSocket;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using MediatR;
using MlkAdmin.Application.Managers.ChannelsManagers.TextChannelsManagers;
using MlkAdmin.Application.Managers.ChannelsManagers.VoiceChannelsManagers;

namespace MlkAdmin.Application.Notifications.Ready
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
