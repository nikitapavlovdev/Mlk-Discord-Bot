using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Discord;
using Discord.WebSocket;
using Discord_Bot.Core.Logs.Log;
using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Core.Notifications.UserJoined;
using Discord_Bot.Core.Notifications.UserLeft;
using Discord_Bot.Core.Notifications.ModalSubmitted;
using Discord_Bot.Core.Notifications.SelectMenuExecuted;
using Discord_Bot.Core.Notifications.ButtonExecuted;
using Discord_Bot.Core.Notifications.GuildAvailable;
using Discord_Bot.Core.Managers.AutorizationManagers;
using Discord_Bot.Core.Managers.ChannelMessageManagers;
using Discord_Bot.Core.Managers.RolesManagers;
using Discord_Bot.Presentation.Controllers.DiscordEventsController;
using Discord_Bot.Presentation.Controllers.DiscordCommandsController;
using Discord_Bot.Infrastructure.Cash;
using Discord_Bot.Core.Notifications.UserVoiceStateUpdated;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace Discord_Bot.Presentation.DiscordAPI
{
    class Startup
    {
        public static async Task Main()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    string? dsconfiguration = Environment.GetEnvironmentVariable("CONFIG_FILE_PATH");
                    if(string.IsNullOrEmpty(dsconfiguration)) 
                    {
                        dsconfiguration = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..")), "Infrastructure", "Configuration", "dsconfiguration.json");
                    }
                    string? channelssettings = Environment.GetEnvironmentVariable("CHANELSSETTINGS_FILE_PATH");
                    if (string.IsNullOrEmpty(channelssettings)) 
                    { 
                        channelssettings = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..")), "Infrastructure", "Configuration", "channelssettings.json"); ; 
                    }
                    string? rolessettings = Environment.GetEnvironmentVariable("ROLESSETTING_FILE_PATH");
                    if (string.IsNullOrEmpty(rolessettings)) 
                    { 
                        rolessettings = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..")), "Infrastructure", "Configuration", "rolessettings.json");
                    }
                    string? emotessettings = Environment.GetEnvironmentVariable("ENOTESSETTINGS_FILE_PATH");
                    if(string.IsNullOrEmpty(emotessettings)) 
                    { 
                        emotessettings = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..")), "Infrastructure", "Configuration", "emotessettings.json");
                    }
                    string? picturesettings = Environment.GetEnvironmentVariable("PICTURESETTINGS_FILE_PATH");
                    if(string.IsNullOrEmpty(picturesettings)) 
                    { 
                        picturesettings = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..")), "Infrastructure", "Configuration", "picturesettings.json");
                    }

                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile(dsconfiguration, optional: false, reloadOnChange: true)
                          .AddJsonFile(channelssettings, optional: false, reloadOnChange: true)
                          .AddJsonFile(rolessettings, optional: false, reloadOnChange: true)
                          .AddJsonFile(emotessettings, optional: false, reloadOnChange: true)
                          .AddJsonFile(picturesettings, optional: false, reloadOnChange: true);
                          
                })
                .ConfigureServices((context, services) =>
                {
                    DiscordSocketConfig _discordSocketConfiguration = new()
                    {
                        GatewayIntents = GatewayIntents.All
                    };

                    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                        typeof(Startup).Assembly,
                        typeof(UserJoinedNotificationHandler).Assembly,
                        typeof(UserLeftNotificationHandler).Assembly,
                        typeof(LogNotificationHandler).Assembly,
                        typeof(ModalSubmittedNotificationHandler).Assembly,
                        typeof(ButtonExecutedNotificationHandler).Assembly,
                        typeof(GuildAvailableNotificationHandler).Assembly,
                        typeof(UserVoiceStateUpdatedNotificationHandler).Assembly,
                        typeof(SelectMenuExecutedNotificationHandler).Assembly));

                    services.AddSingleton(context.Configuration);
                    services.AddSingleton<DiscordEventsController>();
                    services.AddSingleton<DiscordCommandsController>();
                    services.AddSingleton<CommandService>();
                    services.AddSingleton<ExtensionEmbedMessage>();
                    services.AddSingleton<ExtensionSelectionMenu>();
                    services.AddSingleton<ExtensionModal>();
                    services.AddSingleton<ChannelsCash>();
                    services.AddSingleton<RolesCash>();
                    services.AddSingleton<EmotesCash>();
                    services.AddSingleton<AuCash>();
                    services.AddSingleton<RolesManager>();
                    services.AddSingleton<AutorizationManager>();
                    services.AddSingleton<ChannelMessageManager>();
                    services.AddSingleton<ExtensionMessageComponents>();
                    services.AddSingleton(new DiscordSocketClient(_discordSocketConfiguration));
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .Build();

            IConfiguration configuration = host.Services.GetRequiredService<IConfiguration>();
            IMediator mediator = host.Services.GetRequiredService<IMediator>();

            DiscordSocketClient botClient = host.Services.GetRequiredService<DiscordSocketClient>();
            DiscordEventsController eventsController = host.Services.GetRequiredService<DiscordEventsController>();
            DiscordCommandsController commandsController = host.Services.GetRequiredService<DiscordCommandsController>();

            await botClient.LoginAsync(TokenType.Bot, configuration["Token"]);
            await botClient.StartAsync();
            

            await Task.Delay(-1);
        }
    }
}