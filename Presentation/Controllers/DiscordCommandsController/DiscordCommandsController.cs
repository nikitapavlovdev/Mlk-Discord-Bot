using Discord.Commands;
using Discord.WebSocket;
using MediatR;
using System.Reflection;
using Discord;
using MlkAdmin.Core.Providers.JsonProvider;

namespace MlkAdmin.Presentation.Controllers.DiscordCommandsController
{
    public class DiscordCommandsController
    {
        private readonly DiscordSocketClient discordSocketClient;
        private readonly IMediator mediator;
        private readonly JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider;
        public DiscordCommandsController(
            DiscordSocketClient discordSocketClient, 
            CommandService commandService, 
            IServiceProvider serviceProvider,
            IMediator mediator,
            JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider)
        {
            this.discordSocketClient = discordSocketClient;
            this.mediator = mediator;
            this.jsonDiscordConfigurationProvider = jsonDiscordConfigurationProvider;

            discordSocketClient.Ready += OnRegisterCommands;
            //discordSocketClient.SlashCommandExecuted += OnRouteCommand;
            commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
        }

        
        private async Task OnRegisterCommands()
        {
            SocketGuild guild = discordSocketClient.GetGuild(jsonDiscordConfigurationProvider.RootDiscordConfiguration.Guild.Id);

            try
            {
                SlashCommandProperties userInformationCommand = new SlashCommandBuilder()
                    .WithName("get-user-information")
                    .WithDescription("Получить информацию о пользователе")
                    .AddOption("user", type: ApplicationCommandOptionType.User, "Укажите пользователя", isRequired: true)
                    .WithDefaultMemberPermissions(GuildPermission.SendMessages)
                    .WithDefaultMemberPermissions(GuildPermission.UseApplicationCommands)
                    .Build();

                SlashCommandProperties newsMessageCommand = new SlashCommandBuilder()
                    .WithName("send-news-message")
                    .WithDescription("Отправить новостное сообщение")
                    .AddOption("content", type: ApplicationCommandOptionType.String, "Введите новость", isRequired: true)
                    .WithDefaultMemberPermissions(GuildPermission.SendMessages)
                    .WithDefaultMemberPermissions(GuildPermission.UseApplicationCommands)
                    .Build();
                
                await guild.DeleteApplicationCommandsAsync();
                await guild.CreateApplicationCommandAsync(userInformationCommand);
                await guild.CreateApplicationCommandAsync(newsMessageCommand);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //private async Task OnRouteCommand(SocketSlashCommand command)
        //{
        //    //switch (command.CommandName)
        //    //{
        //    //    case "get-user-information":
        //    //        await GetGuildInformationHandle(command);
        //    //        break;

        //    //    case "send-news-message":
        //    //        await SendNewsMessageHandler(command);
        //    //        break;

        //    //    default:
        //    //        break;
        //    //}
        //}

        //private async Task GetGuildInformationHandle(SocketSlashCommand command)
        //{
        //    await mediator.Send(new UserInformationCommand(command));
        //}

        //private async Task SendNewsMessageHandler(SocketSlashCommand command)
        //{
        //    await mediator.Send(new NewsMessageCommand(command));
        //}
    }
}
