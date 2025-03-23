using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Discord_Bot.Infrastructure.Cash;
using Discord_Bot.Core.Utilities.General;

namespace Discord_Bot.Core.Utilities.DI
{
    public class ExtensionEmbedMessage(IConfiguration configuration, RolesCash rolesCash, EmotesCash emotesCash)
    {
        public async Task SendRolesMessage(SocketTextChannel textChannel, MessageComponent component)
        {
            Embed rolesChooseMessage = new EmbedBuilder()
                .WithTitle("Список для выбора")
                .WithDescription(rolesCash.GetDescriptionForСhoiceRoles())
                .WithFooter(ExtensionMethods.GetStringFromConfiguration(configuration["FooterSettings:FooterText"]),
                            ExtensionMethods.GetStringFromConfiguration(configuration["FooterSettings:FooterIconLink"]))
                .WithTimestamp(DateTimeOffset.Now)
                .WithColor(123, 104, 238)
                .WithImageUrl(ExtensionMethods.GetStringFromConfiguration(configuration["Pinterest:Message:LinkPurpleEyes"]))
                .Build();

            await textChannel.SendMessageAsync(embed: rolesChooseMessage, components: component);
        }


        public Embed GetSuccesAuthorizationMessageEmbedTemplate(Emote emoteSuccess, 
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
                .WithText(configuration["FooterSettings:FooterText"])
                .WithIconUrl(configuration["FooterSettings:FooterIconLink"]))
                .Build();

            return message;
        }

        public Embed GetErrorAuthorizationMessageEmbedTemplate(Emote emoteError)
        {
            string title = $"Ошибка\n\n";
            string description = $"**Введен неправильный код, повтори попытку!**{emoteError}";
            Color color = new(220, 20, 60);

            Embed embed = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color)
                .WithFooter(new EmbedFooterBuilder()
                .WithText(configuration["FooterSettings:FooterText"])
                .WithIconUrl(configuration["FooterSettings:FooterIconLink"]))
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
                .WithIconUrl(configuration["FooterSettings:FooterIconLink"]))
                .Build();

            return embed;
        }

        public Embed GetJoinedEmbedTemplate(SocketGuildUser socketGuildUser)
        {
            GuildEmote welcomeMessageEmote = emotesCash.GetEmote(ExtensionMethods.ConvertId(configuration["static:zero_love:id"]));


            string title = $"Новый участник";
            string description = $"Привет, **{socketGuildUser.Username}**! {welcomeMessageEmote}\nДобро пожаловать на сервер **{socketGuildUser.Guild.Name}**" +
                                                                                                        $"\n\nДля продолжения введи код, отправленный тебе в личное сообщение, в форме по кнопочке **«‎Ввести код»‎**";
            Color color = new(30, 144, 255);

            Embed embed = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color)
                .WithAuthor(socketGuildUser.DisplayName, socketGuildUser.GetAvatarUrl(ImageFormat.Auto, 48))
                .WithCurrentTimestamp()
                .WithFooter(new EmbedFooterBuilder()
                .WithText(configuration["FooterSettings:FooterText"])
                .WithIconUrl(configuration["FooterSettings:FooterIconLink"]))
                .Build();

            return embed;
        }

        public Embed GetNewsTamplate(string description)
        {
            Embed embed = new EmbedBuilder()
                .WithTitle("Новости сервера")
                .WithDescription(description)
                .WithColor(135, 206, 250)
                .WithFooter(new EmbedFooterBuilder()
                .WithText(configuration["FooterSettings:FooterText"])
                .WithIconUrl(configuration["FooterSettings:FooterIconLink"]))
                .WithTimestamp(DateTime.UtcNow)
                .Build();

            return embed;
        }


        public static Embed GetGuildUserInformationMessageTemplate(
            string displayname,
            string globalName,
            string userName,
            string avatarId,
            ulong userId,
            int hierarchy,
            bool IsBot,
            IReadOnlyCollection<SocketRole> socketRoles,
            IReadOnlyCollection<ClientType> activeClients,
            IReadOnlyCollection<IActivity> activities,
            DateTimeOffset? premiumSince,
            DateTimeOffset? joinedAt,
            UserStatus userStatus,
            GuildUserFlags flags)
        {
            string blockOfName = $"### Общая информация\n\n" +
                $"Имя пользователя: **{(string.IsNullOrWhiteSpace(userName) ? "Нет данных" : userName)}**\n" +
                $"Имя на сервере: **{(string.IsNullOrWhiteSpace(displayname) ? "Нет данных" : displayname)}**\n" +
                $"Глобальное имя: **{(string.IsNullOrWhiteSpace(globalName) ? "Нет данных" : globalName)}**\n" +
                $"Дата вступления: **{joinedAt.GetValueOrDefault().ToString("D")}**\n" +
                $"Бустер сервера с: {(string.IsNullOrEmpty(premiumSince.GetValueOrDefault().ToString()) ? premiumSince : "**Не является бустером**")}\n";

            string blockOfAdditionInformation = $"### Дополнительная информация\n\n" +
                $"ID пользователя: **{userId}**\n" +
                $"ID аватара: **{avatarId}**\n" +
                $"Пользователь бот: **{(IsBot ? "Да" : "Нет")}**\n" +
                $"Статус пользователя: **{userStatus}**\n" +
                $"Позиция в иерархии ролей: **{hierarchy}**\n";

            string blockOfRoles = "### Роли\n\n" +
                $"Количество ролей: **{socketRoles.Count}**\n";

            foreach(SocketRole role in socketRoles)
            {
                blockOfRoles += $"> {role.Mention}\n";
            }

            string blockOfClients = "### Активные клиенты\n\n" +
                $"Пользователь **{displayname}** активен с **{activeClients.Count}** клиента (-ов)\n";

            foreach(ClientType clientType in activeClients)
            {
                blockOfClients += $"> {clientType}\n";
            }

            string blockOfCurrentActivities = "### Текущие активности\n\n" +
                $"Пользователь **{displayname}** активен в **{activities.Count}** активности (-ях)\n";

            string currentActivities = "";

            foreach (IActivity activity in activities)
            {
                currentActivities += $"> {activity.Type} - {activity.Name}\n";
            }

            string blockOfPublicFlags = "### Флаги\n\n" +
                $"Флаги для пользователя **{displayname}**:\n>>> {flags}\n";

            blockOfCurrentActivities += $"{(activities.Count == 0 ? "> Активностей нет\n" : currentActivities)}";
            
            string ALL_USER_INFO = ""
                + blockOfName
                + blockOfAdditionInformation
                + blockOfRoles 
                + blockOfClients 
                + blockOfCurrentActivities
                + blockOfPublicFlags;

            Embed message = new EmbedBuilder()
                .WithTitle($"Информация об участнике {displayname}")
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
