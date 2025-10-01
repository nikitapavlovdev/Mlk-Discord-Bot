using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._4_Presentation.Discord
{
    public class DiscordSlashCommandAdder(
        ILogger<DiscordSlashCommandAdder> logger, 
        DiscordSocketClient client,
        JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider)
    {
        private List<SlashCommandProperties?> SlashGuildCommands { get; set; } = [];

        public async Task AddCommands()
        {
            try
            {
                ulong guildId = jsonDiscordConfigurationProvider.GuildId;

                await client.Rest.BulkOverwriteGuildCommands([], guildId);


                SlashGuildCommands.Add(AddLobbyNameCommand());
                SlashGuildCommands.Add(GetUserStatistic());

                foreach (SlashCommandProperties? command in SlashGuildCommands)
                {
                    await client.Rest.CreateGuildCommand(command, guildId);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Ошибка в методе AddCommands - {ex}", ex.Message);
            }
        }
        private SlashCommandProperties? AddLobbyNameCommand()
        {
            try
            {
                return new SlashCommandBuilder()
                .WithName("set_lobby")
                .WithDescription("Настраивает имя для создаваемой вами личной комнаты.")
                .AddOption("name", ApplicationCommandOptionType.String, "Имя комнаты", isRequired: true)
                .Build();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return default;
            }

        }

        private SlashCommandProperties? GetUserStatistic()
        {
            try
            {
                return new SlashCommandBuilder()
                .WithName("statistic")
                .WithDescription("Получить мою статистику на сервере")
                .Build();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return default;
            }
        }
    }
}
