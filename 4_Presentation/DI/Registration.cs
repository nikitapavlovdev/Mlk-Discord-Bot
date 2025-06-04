using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using MlkAdmin.Presentation.PresentationServices;
using MlkAdmin.Presentation.DiscordListeners;
using MlkAdmin.Infrastructure.Cache;
using Microsoft.Extensions.Logging;
using MlkAdmin.Application.Managers.ChannelsManagers.TextChannelsManagers;
using MlkAdmin.Application.Managers.ChannelsManagers.VoiceChannelsManagers;
using MlkAdmin.Application.Managers.EmotesManagers;
using MlkAdmin.Application.Managers.RolesManagers;
using MlkAdmin.Application.Managers.UserManagers;
using MlkAdmin.Application.Notifications.ButtonExecuted;
using MlkAdmin.Application.Notifications.GuildAvailable;
using MlkAdmin.Application.Notifications.Log;
using MlkAdmin.Application.Notifications.MessageReceived;
using MlkAdmin.Application.Notifications.ModalSubmitted;
using MlkAdmin.Application.Notifications.ReactionAdded;
using MlkAdmin.Application.Notifications.Ready;
using MlkAdmin.Application.Notifications.SelectMenuExecuted;
using MlkAdmin.Application.Notifications.UserJoined;
using MlkAdmin.Application.Notifications.UserLeft;
using MlkAdmin.Application.Notifications.UserVoiceStateUpdated;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using MlkAdmin.Core.Utilities.DI;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;

namespace MlkAdmin.Presentation.DI
{
    public static class Registration
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddHostedService<DiscordBotHostService>();
            services.AddSingleton<DiscordEventsListener>();
            services.AddSingleton<CommandService>();
            services.AddSingleton(new DiscordSocketClient(new()
            {
                GatewayIntents = GatewayIntents.All
            }
            ));

            return services;
        }
        public static IServiceCollection AddInfastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ChannelsCache>();
            services.AddSingleton<RolesCache>();
            services.AddSingleton<EmotesCache>();
            services.AddSingleton<AutorizationCache>();

            return services;
        }
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(Startup).Assembly,
                typeof(UserJoinedHandler).Assembly,
                typeof(UserLeftHandler).Assembly,
                typeof(LogHandler).Assembly,
                typeof(ModalSubmittedHandler).Assembly,
                typeof(ButtonExecutedHandler).Assembly,
                typeof(GuildAvailableHandler).Assembly,
                typeof(UserVoiceStateUpdatedHandler).Assembly,
                typeof(SelectMenuExecutedHandler).Assembly,
                typeof(ReadyHandler).Assembly,
                typeof(MessageReceivedHandler).Assembly,
                typeof(ReactionAddedHandler).Assembly));

            services.AddSingleton<JsonChannelsMapProvider>(x =>
            {
                return new JsonChannelsMapProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "..", "..", "..", "3_Infrastructure", "Configuration", "DiscordChannelsMap.json")),
                    x.GetRequiredService<ILogger<JsonChannelsMapProvider>>());
            });
            services.AddSingleton<JsonDiscordConfigurationProvider>(x =>
            {
                return new JsonDiscordConfigurationProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "3_Infrastructure", "Configuration", "DiscordConfiguration.json")),
                    x.GetRequiredService<ILogger<JsonDiscordConfigurationProvider>>());
            });
            services.AddSingleton<JsonDiscordEmotesProvider>(x =>
            {
                return new JsonDiscordEmotesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "3_Infrastructure", "Configuration", "DiscordEmotes.json")),
                    x.GetRequiredService<ILogger<JsonDiscordEmotesProvider>>());
            });
            services.AddSingleton<JsonDiscordPicturesProvider>(x =>
            {
                return new JsonDiscordPicturesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "3_Infrastructure", "Configuration", "DiscordPictures.json")),
                    x.GetRequiredService<ILogger<JsonDiscordPicturesProvider>>());
            });
            services.AddSingleton<JsonDiscordRolesProvider>(x =>
            {
                return new JsonDiscordRolesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "..", "..", "..", "3_Infrastructure", "Configuration", "DiscordRoles.json")),
                    x.GetRequiredService<ILogger<JsonDiscordRolesProvider>>());
            });
            services.AddSingleton<JsonDiscordCategoriesProvider>(x =>
            {
                return new JsonDiscordCategoriesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "..", "..", "..", "3_Infrastructure", "Configuration", "DiscordCategoriesMap.json")),
                        x.GetRequiredService<ILogger<JsonDiscordCategoriesProvider>>());
            });
            services.AddSingleton<JsonDiscordDynamicMessagesProvider>(x =>
            {
                return new JsonDiscordDynamicMessagesProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "..", "..", "..", "3_Infrastructure", "Configuration", "DiscordDynamicMessages.json")),
                        x.GetRequiredService<ILogger<JsonDiscordDynamicMessagesProvider>>());
            });
            services.AddSingleton<JsonDiscordUsersLobbyProvider>(x =>
            {
                return new JsonDiscordUsersLobbyProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "..", "..", "..", "3_Infrastructure", "Configuration", "DiscordUsersLobby.json")),
                    x.GetRequiredService<ILogger<JsonDiscordUsersLobbyProvider>>());
            });
            services.AddSingleton<RolesManager>();
            services.AddSingleton<AutorizationManager>();
            services.AddSingleton<VoiceChannelsManager>();
            services.AddSingleton<TextMessageManager>();
            services.AddSingleton<EmotesManager>();
            services.AddSingleton<ModeratorLogsManager>();
            services.AddSingleton<StaticDataServices>();
            services.AddSingleton<EmbedMessageExtension>();
            services.AddSingleton<SelectionMenuExtension>();
            services.AddSingleton<MessageComponentsExtension>();
            services.AddSingleton<ModalExtension>();

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IModeratorLogsSender, ModeratorLogsManager>();

            return services;
        }
    }
}
