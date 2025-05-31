using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MlkAdmin.Core.Managers.ChannelsManagers.TextChannelsManagers;
using MlkAdmin.Core.Managers.ChannelsManagers.VoiceChannelsManagers;
using MlkAdmin.Core.Managers.EmotesManagers;
using MlkAdmin.Core.Managers.RolesManagers;
using MlkAdmin.Core.Managers.UserManagers;
using MlkAdmin.Core.Notifications.ButtonExecuted;
using MlkAdmin.Core.Notifications.GuildAvailable;
using MlkAdmin.Core.Notifications.Log;
using MlkAdmin.Core.Notifications.MessageReceived;
using MlkAdmin.Core.Notifications.ModalSubmitted;
using MlkAdmin.Core.Notifications.ReactionAdded;
using MlkAdmin.Core.Notifications.Ready;
using MlkAdmin.Core.Notifications.SelectMenuExecuted;
using MlkAdmin.Core.Notifications.UserJoined;
using MlkAdmin.Core.Notifications.UserLeft;
using MlkAdmin.Core.Notifications.UserVoiceStateUpdated;
using MlkAdmin.Core.Providers.JsonProvider;
using MlkAdmin.Core.Utilities.DI;
using MlkAdmin.Presentation.API;

namespace MlkAdmin.Core.DependencyInjection
{
    public static class CoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(DiscordStartup).Assembly,
                typeof(UserJoinedNotificationHandler).Assembly,
                typeof(UserLeftNotificationHandler).Assembly,
                typeof(LogNotificationHandler).Assembly,
                typeof(ModalSubmittedNotificationHandler).Assembly,
                typeof(ButtonExecutedNotificationHandler).Assembly,
                typeof(GuildAvailableNotificationHandler).Assembly,
                typeof(UserVoiceStateUpdatedNotificationHandler).Assembly,
                typeof(SelectMenuExecutedNotificationHandler).Assembly,
                typeof(ReadyNotificationHandler).Assembly,
                typeof(MessageReceivedNotificationHandler).Assembly,
                typeof(ReactionAddedNotificationHandler).Assembly));

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
            services.AddSingleton<JsonDiscordUsersLobbyProvider>(x =>
            {
                return new JsonDiscordUsersLobbyProvider(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "..", "..", "..", "Infrastructure", "Configuration", "DiscordUsersLobby.json")),
                    x.GetRequiredService<ILogger<JsonDiscordUsersLobbyProvider>>());
            });
            services.AddSingleton<RolesManager>();
            services.AddSingleton<AutorizationManager>();
            services.AddSingleton<VoiceChannelsManager>();
            services.AddSingleton<TextMessageManager>();
            services.AddSingleton<EmotesManager>();
            services.AddSingleton<ModeratorLogsSender>();
            services.AddSingleton<StaticDataServices>();
            services.AddSingleton<ExtensionEmbedMessage>();
            services.AddSingleton<ExtensionSelectionMenu>();
            services.AddSingleton<ExtensionMessageComponents>();
            services.AddSingleton<ExtensionModal>();

            return services;
        }
    }
}
