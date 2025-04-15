using Discord;

namespace Discord_Bot.Core.Utilities.DI
{
    class ExtensionModal
    {
        public static Modal GetAutorizationModal()
        {
            return new ModalBuilder()
                    .WithTitle($"Имя на сервере")
                    .WithCustomId("au_selection")
                    .AddTextInput(new TextInputBuilder()
                        .WithLabel("Код")
                        .WithCustomId("au_selection_input")
                        .WithMaxLength(10)
                        .WithPlaceholder("Например: WjRokI8xXC")
                        .WithStyle(TextInputStyle.Short))
                    .Build();
        }
        public static Modal GetPersonalInformationModal()
        {
            return new ModalBuilder()
                .WithTitle("Личная информация (Необязательно)")
                .WithCustomId("personal_data")
                .AddTextInput(new TextInputBuilder()
                    .WithLabel("Имя")
                    .WithCustomId("personal_data_input_name")
                    .WithMaxLength(15)
                    .WithPlaceholder("Например: Никита")
                    .WithStyle(TextInputStyle.Short))
                .AddTextInput(new TextInputBuilder()
                    .WithLabel("Дата рождения")
                    .WithCustomId("personal_data_input_dateofbirthday")
                    .WithMaxLength(10)
                    .WithPlaceholder("Формат: 01.01.2001")
                    .WithStyle(TextInputStyle.Short))
                .AddTextInput(new TextInputBuilder()
                    .WithLabel("Страна")
                    .WithCustomId("personal_data_input_country")
                    .WithMaxLength(30)
                    .WithPlaceholder("Например: Россия")
                    .WithStyle(TextInputStyle.Short))
                .AddTextInput(new TextInputBuilder()
                    .WithLabel("Телеграм")
                    .WithCustomId("personal_data_input_telegram")
                    .WithMaxLength(99)
                    .WithPlaceholder("Например: @notnikname")
                    .WithStyle(TextInputStyle.Short))
                .Build();

        }
    }
}
