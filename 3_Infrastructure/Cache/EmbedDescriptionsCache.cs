using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin.Infrastructure.Cache;
using MlkAdmin.Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._3_Infrastructure.Cache
{
    public class EmbedDescriptionsCache(
        RolesCache rolesCache, 
        EmotesCache emotesCache,
        JsonDiscordChannelsMapProvider jsonChannelsMapProvider )
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
            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            string description = "Обязательно к ознакомлению:\n" +
                $"> {jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Rules.Https} - правила сервера.\n" +
                $"> {jsonChannelsMapProvider.RootChannel.Channels.TextChannels.ServerCategory.Roles.Https} - роли сервера.\n\n" +
                $"{pointEmote} **Чтобы завершить верификацию добавьте любую реакцию на этом сообщение!**";

            return description;
        }
        public string GetDescriptionForFeatures()
        {
            GuildEmote? pointEmote = emotesCache.GetEmote("grey_dot");

            string description = $"### ᴋноᴨᴋи\n" +
                    $"**`Моя комната`** - по этой кнопочке Вы можете предложить имя создаваемой вами личной комнаты!" +
                    "\n > Когда вы заходите в канал **➕ | ᴄоздᴀᴛь ᴧобби**, бот автоматически создает вашу личную комнату.\n\n" +
                    $"**`Обо мне`** - по это кнопочке Вы можете отправить свои данные, но только если доверяете Никитке! " +
                    "\n > **Дата рождения** нужна, чтобы я знал, когда Вас поздравлять, а **Имя** - более комфортный формат обращение для меня!\n\n" +
                    $"**`Разраб делай`** - по этой кнопочке вы можете предложить свои квалити оф лайф фичи для сервера!" +
                    "\n > Хочется **обратной связи** от сообщества и послушать Ваши гениальные идеи, а ну.. и попрогать тоже!" +
                    $"\n\n {pointEmote} В будущем, при появление новых функций, они будут появляться именно тут.";

            return description;
        }
    }
}
