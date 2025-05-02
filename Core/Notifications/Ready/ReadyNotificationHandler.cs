using Discord.WebSocket;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;
using Discord_Bot.Core.Managers.ChannelsManagers.VoiceChannelsManagers;
using Discord_Bot.Core.Providers.JsonProvider;
using MediatR;

namespace Discord_Bot.Core.Notifications.Ready
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
                    textMessageManager.SendMessageWithGuildRoles(Guild),
                    textMessageManager.SendRulesMessage(Guild),
                    voiceChannelsManager.ClearTemporaryVoiceChannels(Guild) 
            );
        }
    }
}
