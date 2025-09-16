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
                SlashGuildCommands.Add(AddLobbyNameCommand());

                foreach (SlashCommandProperties? command in SlashGuildCommands)
                {
                    await client.Rest.CreateGuildCommand(command, jsonDiscordConfigurationProvider.GuildId);
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
                .WithName("set_lobby_name")
                .WithDescription("Настраивает имя для создаваемой вами личной комнаты.")
                .AddOption("lobbyname", ApplicationCommandOptionType.String, "Имя комнаты", isRequired: false)
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
