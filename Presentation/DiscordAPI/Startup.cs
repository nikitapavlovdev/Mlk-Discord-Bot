using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Discord;
using Discord.WebSocket;
using Discord_Bot.Core.Utilities.DI;
using Discord_Bot.Core.Notifications.UserJoined;
using Discord_Bot.Core.Notifications.UserLeft;
using Discord_Bot.Core.Notifications.ModalSubmitted;
using Discord_Bot.Core.Notifications.SelectMenuExecuted;
using Discord_Bot.Core.Notifications.ButtonExecuted;
using Discord_Bot.Core.Notifications.GuildAvailable;
using Discord_Bot.Core.Providers.JsonProvider;
using Discord_Bot.Core.Managers.UserManagers;
using Discord_Bot.Core.Managers.RolesManagers;
using Discord_Bot.Presentation.Controllers.DiscordEventsController;
using Discord_Bot.Presentation.Controllers.DiscordCommandsController;
using Discord_Bot.Infrastructure.Cache;
using Discord_Bot.Core.Notifications.UserVoiceStateUpdated;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using Discord_Bot.Core.Notifications.Log;
using Discord_Bot.Core.Managers.ChannelsManagers.TextChannelsManagers;
using Discord_Bot.Core.Managers.ChannelsManagers.VoiceChannelsManagers;

namespace Discord_Bot.Presentation.DiscordAPI
{
    class Startup
    {
        public static async Task Main()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    DiscordSocketConfig discordConfiguration= new()
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

                    services.AddSingleton(new DiscordSocketClient(discordConfiguration));

                    services.AddSingleton<DiscordEventsController>();
                    services.AddSingleton<DiscordCommandsController>();

                    services.AddSingleton<CommandService>();
                    services.AddSingleton<ExtensionEmbedMessage>();
                    services.AddSingleton<ExtensionSelectionMenu>();
                    services.AddSingleton<ExtensionMessageComponents>();
                    services.AddSingleton<ExtensionModal>();

                    services.AddSingleton<ChannelsCache>();
                    services.AddSingleton<RolesCache>();
                    services.AddSingleton<EmotesCache>();
                    services.AddSingleton<AutorizationCache>();

                    services.AddSingleton<RolesManager>();
                    services.AddSingleton<AutorizationManager>();
                    services.AddSingleton<VoiceChannelsManager>();
                    services.AddSingleton<PersonalDataManager>();
                    services.AddSingleton<TextMessageManager>();

                    services.AddSingleton<JsonChannelsMapProvider>(x =>
                    {
                        return new JsonChannelsMapProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, 
                            "..", "..", "..", "Infrastructure", "Configuration", "DiscordChannelsMap.json")),
                            x.GetRequiredService<ILogger<JsonChannelsMapProvider>>());
                    });
                    services.AddSingleton<JsonDiscordConfigurationProvider>(x =>
                    {
                        return new JsonDiscordConfigurationProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Infrastructure", "Configuration", "DiscordConfiguration.json")),
                            x.GetRequiredService<ILogger<JsonDiscordConfigurationProvider>>());
                    });
                    services.AddSingleton<JsonDiscordEmotesProvider>(x =>
                    {
                        return new JsonDiscordEmotesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Infrastructure", "Configuration", "DiscordEmotes.json")),
                            x.GetRequiredService<ILogger<JsonDiscordEmotesProvider>>());
                    });
                    services.AddSingleton<JsonDiscordPicturesProvider>(x =>
                    {
                        return new JsonDiscordPicturesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Infrastructure", "Configuration", "DiscordPictures.json")),
                            x.GetRequiredService<ILogger<JsonDiscordPicturesProvider>>());
                    });
                    services.AddSingleton<JsonDiscordRolesProvider>(x =>
                    {
                        return new JsonDiscordRolesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, 
                            "..", "..", "..", "Infrastructure", "Configuration", "DiscordRoles.json")),
                            x.GetRequiredService<ILogger<JsonDiscordRolesProvider>>());
                    });
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .Build();

            DiscordSocketClient botClient = host.Services.GetRequiredService<DiscordSocketClient>();
            DiscordEventsController eventsController = host.Services.GetRequiredService<DiscordEventsController>();
            DiscordCommandsController commandsController = host.Services.GetRequiredService<DiscordCommandsController>();
            JsonDiscordConfigurationProvider jsonDiscordConfigurationProvider = host.Services.GetRequiredService<JsonDiscordConfigurationProvider>();

            string? _discordToker = jsonDiscordConfigurationProvider.RootDiscordConfiguration?.MalenkieAdminBot?.API_KEY;

            if (_discordToker != null)
            {
                await botClient.LoginAsync(TokenType.Bot, _discordToker);
            }

            await botClient.StartAsync();
            await Task.Delay(-1);
        }
    }
}