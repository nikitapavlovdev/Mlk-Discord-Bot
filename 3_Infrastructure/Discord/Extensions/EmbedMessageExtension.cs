using Discord;
using Discord.WebSocket;
using MlkAdmin._2_Application.DTOs;
using MlkAdmin.Infrastructure.Cache;
using MlkAdmin.Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._3_Infrastructure.Discord.Extensions
{
    public class EmbedMessageExtension(
        EmotesCache emotesCache,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider,
        //JsonDiscordEmotesProvider jsonDiscordEmotesProvider,
        JsonDiscordPicturesProvider jsonDiscordPicturesProvider,
        JsonChannelsMapProvider jsonChannelsMapProvider)
    {
        private readonly string? developer = jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name;
        private readonly string? avatarUrl = jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink;

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
        
        public Embed GetJoinedEmbedTemplate(SocketGuildUser socketGuildUser)    
        {
            //GuildEmote? welcomeMessageEmote = emotesCache.GetEmote(jsonDiscordEmotesProvider.RootDiscordEmotes.StaticEmotes.StaticZero.Love.Id);

            string title = "ᴍᴀʟᴇɴᴋɪᴇ ɴᴇᴡ ᴍᴇᴍʙᴇʀ";
            string description = $"Привет, **{socketGuildUser.Username}**!" +
                $"\nДобро пожаловать на сервер **{socketGuildUser.Guild.Name}**\n\n" +
                $"Для продолжения проследуйте в {jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Hub.Https}";

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
       
        public Embed GetAutoLobbyNamingMessage()
        {
            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            return new EmbedBuilder()
                .WithTitle("ᴍʟᴋ - ɸунᴋции")
                .WithDescription($"### ᴋноᴨᴋи\n" +
                    $"**`Моя комната`** - по этой кнопочке Вы можете предложить имя создаваемой вами личной комнаты!" +
                    "\n > Когда вы заходите в канал **➕ | ᴄоздᴀᴛь ᴧобби**, бот автоматически создает вашу личную комнату.\n\n" +
                    $"**`Обо мне`** - по это кнопочке Вы можете отправить свои данные, но только если доверяете Никитке! " +
                    "\n > **Дата рождения** нужна, чтобы я знал, когда Вас поздравлять, а **Имя** - более комфортный формат обращение для меня!\n\n" +
                    $"**`Разраб делай`** - по этой кнопочке вы можете предложить свои квалити оф лайф фичи для сервера!" +
                    "\n > Хочется **обратной связи** от сообщества и послушать Ваши гениальные идеи, а ну.. и попрогать тоже!" +
                    $"\n\n {pointEmote} В будущем, при появление новых функций, они будут появляться именно тут.")
                .WithColor(232, 228, 225)
                .WithImageUrl(jsonDiscordPicturesProvider.RootDiscordPictures.Pinterest.ForMessage.AutoLobbyNamingMessage)
                .Build();
        }
       
        public Embed GetUserChoiceEmbedTamplate(SocketUser user, string title, string description)
        {
            if(user is SocketGuildUser socketGuildUser)
            {
                return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(218, 247, 166)
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
        public static Embed GetDefaultEmbedTemplate(string title, string descriptions) 
        {
            return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(descriptions)
                .WithColor(0, 193, 255)
                .WithCurrentTimestamp()
                .Build();
        }
    }
};