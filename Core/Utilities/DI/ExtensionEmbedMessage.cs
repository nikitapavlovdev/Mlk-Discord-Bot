using Discord;
using Discord.WebSocket;
using Discord_Bot.Infrastructure.Cache;
using Discord_Bot.Core.Providers.JsonProvider;

namespace Discord_Bot.Core.Utilities.DI
{
    public class ExtensionEmbedMessage(
        RolesCache rolesCachhe, 
        EmotesCache emotesCache,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider,
        JsonDiscordEmotesProvider jsonDiscordEmotesProvider,
        JsonDiscordPicturesProvider jsonDiscordPicturesProvider,
        JsonDiscordRolesProvider jsonDiscordRolesProvider)
    {
        public Embed GetMainRolesEmbedMessage()
        {
            return new EmbedBuilder()
                .WithTitle("ᴍᴀʟᴇɴᴋɪᴇ 🠒 ʀᴏʟᴇs")
                .WithDescription(rolesCachhe.GetDescriptionForMainRoles())
                .WithFooter(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name,
                            jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink)
                .WithColor(50, 50, 53)
                .Build();
        }
        public Embed GetSwitchColorEmbedMessage()
        {
            return new EmbedBuilder()
                .WithTitle("ᴍᴀʟᴇɴᴋɪᴇ 🠒 ɴɪᴄᴋɴᴀᴍᴇ ᴄᴏʟᴏʀ")
                .WithDescription(rolesCachhe.GetDescriptionForSwitchColorRoles())
                .WithFooter(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name,
                            jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink)
                .WithColor(50, 50, 53)
                .Build();
        }
        public Embed GetRulesEmbedMessage()
        {
            return new EmbedBuilder()
               .WithTitle("ᴍᴀʟᴇɴᴋɪᴇ 🠒 ʀᴜʟᴇs")
               .WithDescription(rolesCachhe.GetDescriptionForRules())
               .WithFooter(jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.Name,
                           jsonDiscordConfigurationProvider.RootDiscordConfiguration.DevelopersData.IconLink)
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
        public Embed GetJoinedEmbedTemplate(SocketGuildUser socketGuildUser, string auCode)
        {
            GuildEmote? welcomeMessageEmote = emotesCache.GetEmote(jsonDiscordEmotesProvider.RootDiscordEmotes.StaticEmotes.StaticZero.Love.Id);

            string title = "ᴍᴀʟᴇɴᴋɪᴇ 🠒 ɴᴇᴡ ᴍᴇᴍʙᴇʀ";
            string description = $"Привет, **{socketGuildUser.Username}**! " +
                $"{welcomeMessageEmote}\nДобро пожаловать на сервер **{socketGuildUser.Guild.Name}**" +
                $"\n\nДля продолжения введите код: `{auCode}`";

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