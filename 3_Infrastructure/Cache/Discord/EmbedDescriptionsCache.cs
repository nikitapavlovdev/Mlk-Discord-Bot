using Discord;
using Discord.WebSocket;
using MlkAdmin.Infrastructure.Cache;
using Microsoft.Extensions.DependencyInjection;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using MlkAdmin._3_Infrastructure.JsonModels.Discord.Roles;

namespace MlkAdmin._3_Infrastructure.Cache
{
    public class EmbedDescriptionsCache(
        IServiceProvider serviceProvider,
        RolesCache rolesCache, 
        EmotesCache emotesCache)
    {
        public string GetDiscriptionForMainRoles()
        {
            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            Dictionary<ulong, string> RolesDescriptions = rolesCache.GetDescriptionsForRoles();

            Dictionary<ulong, SocketRole> HierarchyRoles = rolesCache.GetDictonaryWithRoles(RolesDictionaryType.Hierarchy);
            Dictionary<ulong, SocketRole> CategoryRoles = rolesCache.GetDictonaryWithRoles(RolesDictionaryType.Category);
            Dictionary<ulong, SocketRole> UniqieRoles = rolesCache.GetDictonaryWithRoles(RolesDictionaryType.Unique);

            string description = $"В данном блоке представлены все основные роли нашего сервера. " +
                $"Что-то можно выбрать самостоятельно, " +
                $"а что-то получить лично по желанию/на усмотрение администрации!\n";


            description += "### иᴇᴩᴀᴩхия ᴄᴇᴩʙᴇᴩᴀ\n";

            foreach (var role in HierarchyRoles)
            {
                description += $"{pointEmote} {role.Value.Mention} 🠒 {RolesDescriptions[role.Key]}\n";
            }

            description += "### ᴋᴀᴛᴇᴦоᴩии\n";

            foreach (var role in CategoryRoles)
            {
                description += $"{pointEmote} {role.Value.Mention} 🠒 {RolesDescriptions[role.Key]}\n";
            }

            description += "### униᴋᴀᴧьныᴇ ᴩоᴧи\n";

            foreach (var role in UniqieRoles)
            {
                description += $"{pointEmote} {role.Value.Mention} 🠒 {RolesDescriptions[role.Key]}\n";
            }

            return description;
        }
        public string GetDiscriptionForMainRolesNewVers()
        {
            using var scope = serviceProvider.CreateScope();
            JsonDiscordRolesListProvider jsonDiscordRolesListProvider = scope.ServiceProvider.GetRequiredService<JsonDiscordRolesListProvider>();

            GuildEmote pointEmote = emotesCache.GetEmote("grey_dot");

            List<RoleDto> roleDtos = jsonDiscordRolesListProvider.GetRoles();

            string result = $"В данном блоке представлены все основные роли нашего сервера. " +
                $"Что-то можно выбрать самостоятельно, " +
                $"а что-то получить лично по желанию/на усмотрение администрации!\n";

            string serverRolesDescription = $"иᴇᴩᴀᴩхия ᴄᴇᴩʙᴇᴩᴀ\n\n";
            string categoryRolesDescription = $"ᴋᴀᴛᴇᴦоᴩии\n\n";
            string uniqueRolesDescription = $"униᴋᴀᴧьныᴇ ᴩоᴧи\n\n";

            for(int i = 0; i < roleDtos.Count; i++)
            {
                RoleDto role = roleDtos[i];

                string roleLine = $"{pointEmote} <@&{role.Id}> 🠒 {role.Description}\n";

                switch (role.Type)
                {
                    case RoleType.Server:
                        serverRolesDescription += roleLine;
                        break;
                    case RoleType.Category:
                        categoryRolesDescription += roleLine;
                        break;
                    case RoleType.Unique:
                        uniqueRolesDescription += roleLine;
                        break;
                }
            }

            return result+= $"### {serverRolesDescription}\n" +
                $"### {categoryRolesDescription}\n" +
                $"### {uniqueRolesDescription}\n";
        }
        public string GetDescriptionForNameColor()
        {
            Dictionary<ulong, SocketRole> SwitchColorRoles = rolesCache.GetDictonaryWithRoles(RolesDictionaryType.SwitchColor);

            string description = $"В выпадающем меню снизу вы можете выбрать понравившийся цвет для вашего **сервер-нейма**!\n";

            description += "### доᴄᴛуᴨныᴇ цʙᴇᴛᴀ\n\n";

            foreach (var role in SwitchColorRoles)
            {
                description += $"> {role.Value.Mention}\n";
            }

            return description;
        }
        public string GetDescriptionForRules()
        {
            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            string description =
                $"{pointEmote} Внимательно прочтите правила ниже.\n" +
                $"{pointEmote} Никакой чунга-чанги..\n" +
                $"{pointEmote} Будьте искренними с самим собой и вашими собеседниками.\n" +
                $"{pointEmote} Не засоряйте тематические каналы информационным мусором, который никак не связан с темой канала.\n" +
                $"{pointEmote} Постарайтесь уважительно относиться к точке зрения собеседника - у всех нас разный опыт за плечами.\n" +
                $"{pointEmote} Не осуждайте человека за его ошибки. Постарайтесь понять корень проблемы прежде чем делать выводы.\n" +
                $"{pointEmote} Не обсуждайте мировую политику и не создавайте ситуационных споров на этой почве.\n" +
                $"{pointEmote} Постарайтесь не выливать весь негатив на ваших собеседников. Либо делайте это, но с заранее выключеным микрофоном.\n" +
                $"{pointEmote} Будьте самими собою!\n" +
                $"{pointEmote} Не стесняйтесь просить помощи у других.\n" +
                $"{pointEmote} Не стоит быть чересчур навязчивым.\n" +
                $"{pointEmote} А это правило существует, чисто чтобы проверить команду!\n\n";

            description += "И самое главное - наслаждайтесь моментом!";

            return description;
        }
        public string GetDescriptionForAutorization()
        {
            using var scope = serviceProvider.CreateScope();
            JsonDiscordChannelsMapProvider jsonChannelsMapProvider = scope.ServiceProvider.GetRequiredService<JsonDiscordChannelsMapProvider>();

            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            string description = "Обязательно к ознакомлению:\n" +
                $"> {jsonChannelsMapProvider.RulesChannelHttps} - правила сервера.\n" +
                $"> {jsonChannelsMapProvider.RolesChannelHttps} - роли сервера.\n\n" +
                $"{pointEmote} **Чтобы завершить верификацию добавьте любую реакцию на этом сообщение!**";

            return description;
        }
        public string GetDescriptionForServerPeculiarities()
        {
            return $"### 1. Случайное название авто-лобби\n" +
                $"При создании личной комнаты, с малой долей вероятности Вам может выпасть случайное название создаваемого канала!\n\n" +
                $"Уникальные названия и шанс замены имени комнаты: \n" +
                $"> — _million amnymchik kid c шансом **1 к 1.000.000**_;\n" +
                $"> — _one hundred thousand kid с шансом **1 к 100.000**_;\n" +
                $"> — _one thousand kid с шансом **1 к 1.000**_;\n\n" +
                $"Идея в том, что если Вам выпадет одно из этих названий - Вы получаете соответствующую награду! Проблема, что она должна быть ценна из-за редкости события, но пока я ничего не смог придумать. Как заработаю денег, так сразу!\n" +
                $"### 2. Гача Бот\n" +
                $"Дабы разнообразить нашу деятельность внутри Discord было принято решение разработать гачи-бота, через который можно внутри сервера крутить персонажей!\n\n" +
                $"Бот позволяет: \n" +
                $"> — _накапливать валюту путем активности на сервере_\n" +
                $"> — _тратить валюту на пулы_\n" +
                $"> — _просматривать коллекцию персонажей_\n" +
                $"> — _получать уникальные роли сервера за коллекцию персонажей_\n" +
                $"> — _и многое другое!_\n\n" +
                $"Идея в том, чтобы просто разнообразить времяпровождение на сервере путем внедрения геймификации опираясь на излюбленный концепт гача-игр!";
        }
    }
}
