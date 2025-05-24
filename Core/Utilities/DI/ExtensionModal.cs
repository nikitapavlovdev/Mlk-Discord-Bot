using Discord;

namespace MlkAdmin.Core.Utilities.DI
{
    class ExtensionModal
    {
        public static Modal GetPersonalInformationModal()
        {
            return new ModalBuilder()
                .WithTitle("Обо мне")
                .WithCustomId("personal_data_modal")
                .AddTextInput(new TextInputBuilder()
                    .WithLabel("Ваше имя")
                    .WithCustomId("personal_data_input_name")
                    .WithMaxLength(15)
                    .WithPlaceholder("Например: Никитка")
                    .WithStyle(TextInputStyle.Short))
                .AddTextInput(new TextInputBuilder()
                    .WithLabel("Дата рождения")
                    .WithCustomId("personal_data_input_dateofbirthday")
                    .WithMaxLength(10)
                    .WithPlaceholder("Формат: 01.01.2001")
                    .WithStyle(TextInputStyle.Short))
                .Build();
        }
        public static Modal GetLobbyNamingModal()
        {
            return new ModalBuilder()
                .WithTitle("Моя комната")
                .WithCustomId("lobby_naming_modal")
                .AddTextInput(new TextInputBuilder()
                    .WithLabel("Имя комнаты")
                    .WithCustomId("lobby_naming_input_name")
                    .WithMaxLength(20)
                    .WithPlaceholder("Например: Ронинская пятка")
                    .WithStyle(TextInputStyle.Short))
                .Build();
        }
        public static Modal GetFeedBackModal()
        {
            return new ModalBuilder()
                .WithTitle("Разраб делай")
                .WithCustomId("feedback_modal")
                .AddTextInput(new TextInputBuilder()
                    .WithLabel("Идеи / предложения")
                    .WithCustomId("feedback_input_feedback")
                    .WithMaxLength(4000)
                    .WithPlaceholder("Хочу, чтобы бот Ронина тегал и писал, что он лошара!")
                    .WithStyle(TextInputStyle.Paragraph))
                .Build();
        }
    }
}
