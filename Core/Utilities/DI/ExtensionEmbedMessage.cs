using Discord;
using Discord.WebSocket;
using MlkAdmin.Infrastructure.Cache;
using MlkAdmin.Core.Providers.JsonProvider;

namespace MlkAdmin.Core.Utilities.DI
{
    public class ExtensionEmbedMessage(
        RolesCache rolesCachhe, 
        EmotesCache emotesCache,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider,
        JsonDiscordEmotesProvider jsonDiscordEmotesProvider,
        JsonDiscordPicturesProvider jsonDiscordPicturesProvider,
        JsonDiscordRolesProvider jsonDiscordRolesProvider,
        JsonChannelsMapProvider jsonChannelsMapProvider)
    {
        private readonly string? developer = jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name;
        private readonly string? avatarUrl = jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink;

        public Embed GetMainRolesEmbedMessage()
        {
            return new EmbedBuilder()
                .WithTitle("ᴍᴀʟᴇɴᴋɪᴇ ʀᴏʟᴇs")
                .WithDescription(rolesCachhe.GetDescriptionForMainRoles())
                .WithFooter(developer, avatarUrl)
                .WithColor(50, 50, 53)
                .Build();
        }
        public Embed GetSwitchColorEmbedMessage()
        {
            return new EmbedBuilder()
                .WithTitle("ɴɪᴄᴋɴᴀᴍᴇ ᴄᴏʟᴏʀ")
                .WithDescription(rolesCachhe.GetDescriptionForSwitchColorRoles())
                .WithFooter(developer, avatarUrl)
                .WithColor(50, 50, 53)
                .Build();
        }
        public Embed GetRulesEmbedMessage()
        {
            return new EmbedBuilder()
               .WithTitle("ᴍᴀʟᴇɴᴋɪᴇ ʀᴜʟᴇs")
               .WithDescription(rolesCachhe.GetDescriptionForRules())
               .WithFooter(developer, avatarUrl)
               .WithColor(50, 50, 53)
               .WithImageUrl(jsonDiscordPicturesProvider.RootDiscordPictures.Pinterest.ForMessage.RulesBanner)
               .Build();
        }
        public Embed GetSuccesAuthorizationMessageEmbedTemplate()
        {
            string title = $"Успех\n\n";

            string description = $"**Верификация пройдена успешно!**{emotesCache.GetEmote(jsonDiscordEmotesProvider.RootDiscordEmotes.StaticEmotes.StaticZero.Love.Id)}\n\n" +
                            $"Добавлена базовая роль: {rolesCachhe.GetRole(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Autorization.MalenkiyMember.Id).Mention}\n" +
                            $"Добавлена игровая роль: {rolesCachhe.GetRole(jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Gamer.Id).Mention}";

            Color color = new(0, 255, 127);

            Embed message = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color)
                .WithTimestamp(DateTime.UtcNow)
                .WithFooter(developer, avatarUrl)
                .Build();

            return message;
        }
        public Embed GetErrorAuthorizationMessageEmbedTemplate(Emote? emoteError)
        {
            string title = $"Ошибка\n\n";
            string description = $"**Введен неправильный код, повтори попытку!**{emoteError}";
            Color color = new(220, 20, 60);

            Embed embed = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color)
                .WithFooter(developer, avatarUrl)
                .Build();

            return embed;
        }
        public Embed GetErrorDateMessageEmbedTemplate()
        {
            Color color = new(178, 34, 34);

            string title = "Ошибка ввода даты";

            string description = "Введенная дата не соответствует представленному шаблону!\n " +
                "Учти, что формат определяется исходя из российского региона в виде 01.02.2003, где: \n\n" +
                "01 - день месяца\n" +
                "02 - месяц\n" +
                "2003 - год\n\n" +
                "Обязательно разделить точками!\n " +
                "Пока что я не придумал, как сделать выбор сразу нужного типа, чтобы не вводить это строкой :)";

            string footerText = "Повтори попытку исходя из описанного выше";

            Embed embed = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color)
                .WithFooter(new EmbedFooterBuilder()
                .WithText(footerText)
                .WithIconUrl(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink))
                .Build();

            return embed;
        }
        public Embed GetJoinedEmbedTemplate(SocketGuildUser socketGuildUser)
        {
            GuildEmote? welcomeMessageEmote = emotesCache.GetEmote(jsonDiscordEmotesProvider.RootDiscordEmotes.StaticEmotes.StaticZero.Love.Id);

            string title = "ᴍᴀʟᴇɴᴋɪᴇ ɴᴇᴡ ᴍᴇᴍʙᴇʀ";
            string description = $"Привет, **{socketGuildUser.Username}**! " +
                $"{welcomeMessageEmote}\nДобро пожаловать на сервер **{socketGuildUser.Guild.Name}**";

            Embed embed = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(new(30, 144, 255))
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
        public Embed GetGuildUserInformationMessageTemplate(SocketGuildUser socketGuildUser)
        {
            string blockOfName = $"### общᴀя инɸоᴩʍᴀция\n\n" +
                $"> Имя пользователя: **{(string.IsNullOrWhiteSpace(socketGuildUser.Username) ? "Нет данных" : socketGuildUser.Username)}**\n" +
                $"> Имя на сервере: **{(string.IsNullOrWhiteSpace(socketGuildUser.DisplayName) ? "Нет данных" : socketGuildUser.DisplayName)}**\n" +
                $"> Глобальное имя: **{(string.IsNullOrWhiteSpace(socketGuildUser.GlobalName) ? "Нет данных" : socketGuildUser.GlobalName)}**\n" +
                $"> Дата вступления: **{socketGuildUser.JoinedAt.GetValueOrDefault():D}**\n";

            string blockOfAdditionInformation = $"### доᴨоᴧниᴛᴇᴧьнᴀя инɸоᴩʍᴀция\n\n" +
                $"> ID пользователя: **{socketGuildUser.Id}**\n" +
                $"> ID аватара: **{socketGuildUser.AvatarId}**\n" +
                $"> Пользователь бот: **{(socketGuildUser.IsBot ? "Да" : "Нет")}**\n" +
                $"> Статус пользователя: **{socketGuildUser.Status}**\n";

            string blockOfClients = "### Активные клиенты\n\n" +
                $"> Пользователь **{socketGuildUser.DisplayName}** активен с **{socketGuildUser.ActiveClients.Count}** клиента (-ов)\n";

            foreach(ClientType clientType in socketGuildUser.ActiveClients)
            {
                blockOfClients += $"> {clientType}\n";
            }

            string blockOfPublicFlags = "### ɸᴧᴀᴦи\n\n" +
                $"Флаги для пользователя **{socketGuildUser.DisplayName}**:\n>>> {socketGuildUser.Flags}\n";

            string general = ""
                + blockOfName
                + blockOfAdditionInformation
                + blockOfClients 
                + blockOfPublicFlags;

            Embed message = new EmbedBuilder()
                .WithTitle("ᴜsᴇʀ ɪɴꜰᴏʀᴍᴀᴛɪᴏɴ")
                .WithAuthor(socketGuildUser.DisplayName, socketGuildUser.GetAvatarUrl(ImageFormat.Auto, 48))
                .WithDescription(general)
                .WithColor(30, 144, 255)
                .WithCurrentTimestamp()
                .Build();

            return message;
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
        public Embed GetAutorizationReactionMessage()
        {
            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            return new EmbedBuilder()
                .WithTitle("ᴍʟᴋ - ʙᴇᴩиɸиᴋᴀция")
                .WithDescription("Обязательно к ознакомлению:\n" +
                $"> {jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Rules.Https} - правила сервера.\n" +
                $"> {jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Https} - роли сервера.\n\n" +
                $"{pointEmote} **Чтобы завершить верификацию добавьте любую реакцию на этом сообщение!**")
                .WithColor(232, 228, 225)
                .WithImageUrl(jsonDiscordPicturesProvider.RootDiscordPictures.Pinterest.ForMessage.AuMessage)
                .Build();
        }
        public Embed GetUserChoiceEmbedTamplate(SocketUser user, string title, string description)
        {
            if(user is SocketGuildUser socketGuildUser)
            {
                return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(Color.Default)
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