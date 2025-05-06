using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Core.Providers.JsonProvider;
using Discord.WebSocket;

namespace Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers
{
    public class ModeratorLogsSender(
        JsonChannelsMapProvider jsonChannelsMapProvider)
    {
        public async Task SendRemovingVoiceChannelMessage(SocketVoiceChannel socketVoiceChannel, SocketGuild guild, string fromClass, string fromMethod)
        {
            string descriptions = $"> Канал: **{socketVoiceChannel.Name}**\n > Класс: **{fromClass}**\n > Метод: **{fromMethod}**";
            SocketTextChannel? logsChannel = guild.TextChannels.FirstOrDefault(x => x.Id == jsonChannelsMapProvider.RootChannel.Channels.TextChannels.AdministratorCategory.Logs.Id);


            await logsChannel.SendMessageAsync(embed: ExtensionEmbedMessage.GetDefaultEmbedTemplate("ʀᴇᴍᴏᴠɪɴɢ ᴄʜᴀɴɴᴇʟ", descriptions));
        }
    }
}
