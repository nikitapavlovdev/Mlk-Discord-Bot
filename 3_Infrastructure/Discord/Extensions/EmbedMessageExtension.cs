using Discord;
using Discord.WebSocket;
using MlkAdmin._2_Application.DTOs;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._3_Infrastructure.Discord.Extensions
{
    public class EmbedMessageExtension(
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider)
    {
        private readonly string? developer = jsonDiscordConfigurationProvider.DevName;
        private readonly string? avatarUrl = jsonDiscordConfigurationProvider.DevIconUrl;

        public Embed GetDynamicMessageEmbedTamplate(EmbedDto embedDto)
        {
             return new EmbedBuilder()
                .WithTitle(embedDto.Title)
                .WithDescription(embedDto.Description)
                .WithFooter(developer, avatarUrl)
                .WithColor(50, 50, 53)  
                .WithImageUrl(embedDto.PicturesUrl)
                .Build();
        }

        public Embed GetStaticMessageEmbedTamplate(EmbedDto embedDto)
        {
            return new EmbedBuilder()
                .WithTitle(embedDto.Title)
                .WithDescription(embedDto.Description)
                .WithColor(50, 50, 53)
                .WithImageUrl(embedDto.PicturesUrl)
                .Build();
        }
        
        public Embed GetJoinedEmbedTemplate(SocketGuildUser socketGuildUser)    
        {
            string title = "ᴍᴀʟᴇɴᴋɪᴇ ɴᴇᴡ ᴍᴇᴍʙᴇʀ";
            string description = $"Привет, **{socketGuildUser.Username}**!" +
                $"\nДобро пожаловать на сервер **{socketGuildUser.Guild.Name}**\n\n" +
                $"Для продолжения проследуйте в {jsonChannelsMapProvider.HubChannelHttps}";

            Embed embed = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(30, 144, 255)
                .WithFooter(new EmbedFooterBuilder()
                    .WithText(socketGuildUser.DisplayName)
                    .WithIconUrl(socketGuildUser.GetAvatarUrl(ImageFormat.Auto, 48)))
                .Build();

            return embed;
        }
        public Embed GetFarewellEmbedTamplate(SocketUser socketUser)
        {
            string title = "ᴍᴀʟᴇɴᴋɪᴇ 🠒 ᴍᴇᴍʙᴇʀ ʟᴇꜰᴛ";
            string description = $"Пользовтель {socketUser.Mention} покинул сервер.";

            return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(new(240, 128, 128))
                .WithCurrentTimestamp()
                .WithFooter(developer, avatarUrl)
                .Build();
        }
        public Embed GetNewsTamplate(string description)
        {
            Embed embed = new EmbedBuilder()
                .WithTitle("Новости сервера")
                .WithDescription(description)
                .WithColor(135, 206, 250)
                .WithFooter(developer, avatarUrl)
                .WithTimestamp(DateTime.UtcNow)
                .Build();

            return embed;
        }
       
        public Embed GetUserChoiceEmbedTamplate(SocketUser user, string title, string description)
        {
            if(user is SocketGuildUser socketGuildUser)
            {
                return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(50, 50, 53)
                .WithAuthor(socketGuildUser.DisplayName, socketGuildUser.GetAvatarUrl(ImageFormat.Auto, 48))
                .Build();
            }

            return new EmbedBuilder().Build();
        }
        public static Embed GetNoAccessTemplate()
        {
            Embed message = new EmbedBuilder()
                .WithTitle("Не так быстро!")
                .WithDescription("Команда предназначена для создателя и модераторов сообщества!")
                .WithColor(Color.Red)
                .WithCurrentTimestamp()
                .Build();

            return message;
        }
        public static Embed GetExceptionsMessageTamplate(string description, string method)
        {
            Embed embed = new EmbedBuilder()
                .WithTitle("Exception")
                .WithDescription($"{method}\n\n{description}")
                .WithColor(Color.Red)
                .WithTimestamp(DateTimeOffset.Now)
                .Build();

            return embed;
        }
        public static Embed GetDefaultEmbedTemplate(string title, string descriptions, Color color = default) 
        {
            if (color == default) { color = new Color(50, 50, 53); }

            return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(descriptions)
                .WithColor(color)
                .WithCurrentTimestamp()
                .Build();
        }
    }
};