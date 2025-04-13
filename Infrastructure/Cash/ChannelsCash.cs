using System.Reactive;
using Discord;
using Discord.WebSocket;
using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Core.Utilities.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Infrastructure.Cash
{
    public class ChannelsCash(IConfiguration configuration, 
        ILogger<ChannelsCash> _logger,
        ExtensionEmbedMessage embedMessage,
        ExtensionSelectionMenu selectionMenu)
    {
        private readonly List<ulong> GenereatingChannelsIds = [];
        private readonly List<ulong> TemporaryChannelIds = [];
        private readonly Dictionary<ulong, string> CategoryNameFromId = [];


        public async Task ChannelsInitialization(SocketGuild socketGuild)
        {
            try
            {
                await Task.WhenAll(
                FillCategoryFromName(),
                SetGeneratingChannelsId()
                );

                await SendFormInRolesChannel(socketGuild);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}", ex.Message);
            }
        }
        private async Task SendFormInRolesChannel(SocketGuild socketGuild)
        {
            SocketTextChannel? textChannel = socketGuild.TextChannels.FirstOrDefault(x => x.Id == (ExtensionMethods.ConvertId(configuration["RolesSettings:ChannelId"])));
            MessageComponent component = selectionMenu.GetRolesSelectionMenu();

            if (textChannel == null)
            {
                return;
            }

            await ExtensionChannelsManager.DeleteAllMessageFromChannel(textChannel);
            await embedMessage.SendRolesMessage(textChannel, component);
        }

        public bool IsGeneratingChannel(ulong channelId)
        {
            return GenereatingChannelsIds.Any(x => x == channelId);
        }

        public bool IsTemporaryChannel(ulong channelId)
        {
            return TemporaryChannelIds.Any(x => x == channelId);
        }

        public void AddTemporaryChannelInList(IVoiceChannel channel)
        {
            TemporaryChannelIds.Add(channel.Id);
        }

        public void DeleteTemoraryChannelFromList(IVoiceChannel channel)
        {
            TemporaryChannelIds.Remove(channel.Id);
        }

        private async Task FillCategoryFromName()
        {
            try
            {
                CategoryNameFromId.TryAdd(ExtensionMethods.ConvertId(configuration["GameCategory:Id"]), "ɢᴀᴍᴇᴠᴏɪᴄᴇ");
                
                await Task.CompletedTask;
            }
            catch (Exception)
            {

            }
        }

        private async Task SetGeneratingChannelsId()
        {
            GenereatingChannelsIds.Add(ExtensionMethods.ConvertId(configuration["AutoLobby:CategoryGames:Id"]));

            await Task.CompletedTask;
        }
    }
}
