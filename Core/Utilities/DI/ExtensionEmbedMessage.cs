using Discord;
using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cache;
using Discord_Bot.Core.Utilities.General;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Core.Utilities.DI
{
    public class ExtensionEmbedMessage(
        RolesCache rolesCachhe, 
        EmotesCache emotesCache,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider,
        JsonDiscordEmotesProvider jsonDiscordEmotesProvider,
        JsonDiscordPicturesProvider jsonDiscordPicturesProvider)
    {
        public Embed GetMainRolesEmbedMessage()
        {
            return new EmbedBuilder()
                .WithTitle("ᴍᴀʟᴇɴᴋɪᴇ 🠒 ʀᴏʟᴇs")
                .WithDescription(rolesCachhe.GetDescriptionForMainRoles())
                .WithFooter(ExtensionMethods.GetStringFromConfiguration(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name),
                            ExtensionMethods.GetStringFromConfiguration(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink))
                .WithTimestamp(DateTimeOffset.Now)
                .WithColor(19, 20, 22)
                .WithImageUrl(jsonDiscordPicturesProvider.RootDiscordPictures.Pinterest.ForMessage.RolesBanner)
                .Build();
        }
        public Embed GetSwitchColorEmbedMessage()
        {
            return new EmbedBuilder()
                .WithTitle("ᴍᴀʟᴇɴᴋɪᴇ 🠒 ɴɪᴄᴋɴᴀᴍᴇ ᴄᴏʟᴏʀ")
                .WithDescription(rolesCachhe.GetDescriptionForSwitchColorRoles())
                .WithFooter(ExtensionMethods.GetStringFromConfiguration(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name),
                            ExtensionMethods.GetStringFromConfiguration(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink))
                .WithTimestamp(DateTimeOffset.Now)
                .WithColor(19, 20, 22)
                .WithImageUrl(jsonDiscordPicturesProvider.RootDiscordPictures.Pinterest.ForMessage.ColorNameBanner)
                .Build();
        }
        public Embed GetRulesEmbedMessage()
        {
            return new EmbedBuilder()
               .WithTitle("ᴍᴀʟᴇɴᴋɪᴇ 🠒 ʀᴜʟᴇs")
               .WithDescription(rolesCachhe.GetDescriptionForRules())
               .WithFooter(ExtensionMethods.GetStringFromConfiguration(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name),
                           ExtensionMethods.GetStringFromConfiguration(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink))
               .WithTimestamp(DateTimeOffset.Now)
               .WithColor(19, 20, 22)
               .WithImageUrl(jsonDiscordPicturesProvider.RootDiscordPictures.Pinterest.ForMessage.RulesBanner)
               .Build();
        }
        public Embed GetSuccesAuthorizationMessageEmbedTemplate(Emote? emoteSuccess, 
            SocketRole baseServerRole, 
            ulong roleChannelId, 
            ulong botCommandChannelId, 
            ulong newsChannelId)
        {
            string title = $"Успех\n\n";

            string description = $"**Верификация пройдена успешно!**{emoteSuccess}\n\n" +
                            $"Добавлена базовая роль: {baseServerRole.Mention}\n\n" +
                            $"<#{roleChannelId}>         - получить интересующие роли\n" +
                            $"<#{botCommandChannelId}>   - канал для команд бота\n" +
                            $"<#{newsChannelId}>         - общий новостной канал сервера\n";

            Color color = new(0, 255, 127);

            Embed message = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color)
                .WithTimestamp(DateTime.UtcNow)
                .WithFooter(new EmbedFooterBuilder()
                .WithText(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name)
                .WithIconUrl(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink))
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
                .WithFooter(new EmbedFooterBuilder()
                .WithText(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name)
                .WithIconUrl(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink))
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


            string title = "Новый участник";
            string description = $"Привет, **{socketGuildUser.Username}**! {welcomeMessageEmote}\nДобро пожаловать на сервер **{socketGuildUser.Guild.Name}**" +
                                                                                                        $"\n\nДля продолжения введи код, отправленный тебе в личное сообщение, в форме по кнопочке **«‎Ввести код»‎**";
            Embed embed = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(new(30, 144, 255))
                .WithAuthor(socketGuildUser.DisplayName, socketGuildUser.GetAvatarUrl(ImageFormat.Auto, 48))
                .WithCurrentTimestamp()
                .WithFooter(new EmbedFooterBuilder()
                .WithText(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name)
                .WithIconUrl(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink))
                .Build();

            return embed;
        }
        public Embed GetFarewellEmbedTamplate(SocketUser socketUser)
        {
            string title = "Покинувший сервер";
            string description = $"Пользовтель {socketUser.Mention} покинул сервер.";

            return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(new(240, 128, 128))
                .WithCurrentTimestamp()
                .WithFooter(new EmbedFooterBuilder()
                    .WithText(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name)
                    .WithIconUrl(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink))
                .Build();
        }
        public Embed GetNewsTamplate(string description)
        {
            Embed embed = new EmbedBuilder()
                .WithTitle("Новости сервера")
                .WithDescription(description)
                .WithColor(135, 206, 250)
                .WithFooter(new EmbedFooterBuilder()
                .WithText(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name)
                .WithIconUrl(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink))
                .WithTimestamp(DateTime.UtcNow)
                .Build();

            return embed;
        }
        public Embed GetGuildUserInformationMessageTemplate(SocketGuildUser socketGuildUser)
        {
            string blockOfName = $"### Общая информация\n\n" +
                $"Имя пользователя: **{(string.IsNullOrWhiteSpace(socketGuildUser.Username) ? "Нет данных" : socketGuildUser.Username)}**\n" +
                $"Имя на сервере: **{(string.IsNullOrWhiteSpace(socketGuildUser.DisplayName) ? "Нет данных" : socketGuildUser.DisplayName)}**\n" +
                $"Глобальное имя: **{(string.IsNullOrWhiteSpace(socketGuildUser.GlobalName) ? "Нет данных" : socketGuildUser.GlobalName)}**\n" +
                $"Дата вступления: **{socketGuildUser.JoinedAt.GetValueOrDefault():D}**\n" +
                $"Бустер сервера с: {(string.IsNullOrEmpty(socketGuildUser.PremiumSince.GetValueOrDefault().ToString()) ? socketGuildUser.PremiumSince : "**Не является бустером**")}\n";

            string blockOfAdditionInformation = $"### Дополнительная информация\n\n" +
                $"ID пользователя: **{socketGuildUser.Id}**\n" +
                $"ID аватара: **{socketGuildUser.AvatarId}**\n" +
                $"Пользователь бот: **{(socketGuildUser .IsBot ? "Да" : "Нет")}**\n" +
                $"Статус пользователя: **{socketGuildUser.Status}**\n" +
                $"Позиция в иерархии ролей: **{socketGuildUser.Hierarchy}**\n";

            string blockOfClients = "### Активные клиенты\n\n" +
                $"Пользователь **{socketGuildUser.DisplayName}** активен с **{socketGuildUser.ActiveClients.Count}** клиента (-ов)\n";

            foreach(ClientType clientType in socketGuildUser.ActiveClients)
            {
                blockOfClients += $"> {clientType}\n";
            }

            string blockOfPublicFlags = "### Флаги\n\n" +
                $"Флаги для пользователя **{socketGuildUser.DisplayName}**:\n>>> {socketGuildUser.Flags}\n";

            string ALL_USER_INFO = ""
                + blockOfName
                + blockOfAdditionInformation
                + blockOfClients 
                + blockOfPublicFlags;

            Embed message = new EmbedBuilder()
                .WithTitle($"Информация об участнике {socketGuildUser.DisplayName}")
                .WithDescription(ALL_USER_INFO)
                .WithColor(30, 144, 255)
                .WithCurrentTimestamp()
                .Build();

            return message;
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
    }
};
