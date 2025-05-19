using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Discord;
using Discord.WebSocket;
using MlkAdmin.Core.Utilities.DI;
using MlkAdmin.Core.Notifications.UserJoined;
using MlkAdmin.Core.Notifications.UserLeft;
using MlkAdmin.Core.Notifications.ModalSubmitted;
using MlkAdmin.Core.Notifications.SelectMenuExecuted;
using MlkAdmin.Core.Notifications.ButtonExecuted;
using MlkAdmin.Core.Notifications.GuildAvailable;
using MlkAdmin.Core.Providers.JsonProvider;
using MlkAdmin.Core.Managers.UserManagers;
using MlkAdmin.Core.Managers.RolesManagers;
using MlkAdmin.Presentation.Controllers.DiscordEventsController;
using MlkAdmin.Presentation.Controllers.DiscordCommandsController;
using MlkAdmin.Infrastructure.Cache;
using MlkAdmin.Core.Notifications.UserVoiceStateUpdated;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Notifications.Log;
using MlkAdmin.Core.Managers.ChannelsManagers.TextChannelsManagers;
using MlkAdmin.Core.Managers.ChannelsManagers.VoiceChannelsManagers;
using MlkAdmin.Core.Managers.EmotesManagers;
using MlkAdmin.Core.Notifications.Ready;
using MlkAdmin.Core.Notifications.MessageReceived;

namespace MlkAdmin.Presentation.DiscordAPI
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
                        typeof(SelectMenuExecutedNotificationHandler).Assembly,
                        typeof(ReadyNotificationHandler).Assembly,
                        typeof(MessageReceivedNotificationHandler).Assembly));

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
                    services.AddSingleton<EmotesManager>();
                    services.AddSingleton<ModeratorLogsSender>();

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
                    services.AddSingleton<JsonDiscordCategoriesProvider>(x =>
                    {
                        return new JsonDiscordCategoriesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                            "..", "..", "..", "Infrastructure", "Configuration", "DiscordCategoriesMap.json")),
                                x.GetRequiredService<ILogger<JsonDiscordCategoriesProvider>>());
                    });
                    services.AddSingleton<JsonDiscordDynamicMessagesProvider>(x =>
                    {
                        return new JsonDiscordDynamicMessagesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                            "..", "..", "..", "Infrastructure", "Configuration", "DiscordDynamicMessages.json")),
                                x.GetRequiredService<ILogger<JsonDiscordDynamicMessagesProvider>>());
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

            string? discordToken = jsonDiscordConfigurationProvider.RootDiscordConfiguration?.MalenkieAdminBot?.API_KEY;

            if (discordToken != null)
            {
                await botClient.LoginAsync(TokenType.Bot, discordToken);
            }

            await botClient.StartAsync();
            await Task.Delay(-1);
        }
    }
}