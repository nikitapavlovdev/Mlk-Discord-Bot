using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using MlkAdmin.Presentation.PresentationServices;
using MlkAdmin.Presentation.DiscordListeners;
using MlkAdmin.Infrastructure.Cache;
using MlkAdmin._2_Application.Managers.Channels.TextChannelsManagers;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannelsManagers;
using MlkAdmin._2_Application.Managers.EmotesManagers;
using MlkAdmin._2_Application.Managers.RolesManagers;
using MlkAdmin._2_Application.Managers.UserManagers;
using MlkAdmin._2_Application.Notifications.ButtonExecuted;
using MlkAdmin._2_Application.Notifications.GuildAvailable;
using MlkAdmin._2_Application.Notifications.Log;
using MlkAdmin._2_Application.Notifications.MessageReceived;
using MlkAdmin._2_Application.Notifications.ModalSubmitted;
using MlkAdmin._2_Application.Notifications.ReactionAdded;
using MlkAdmin._2_Application.Notifications.Ready;
using MlkAdmin._2_Application.Notifications.SelectMenuExecuted;
using MlkAdmin._2_Application.Notifications.UserJoined;
using MlkAdmin._2_Application.Notifications.UserLeft;
using MlkAdmin._2_Application.Notifications.UserVoiceStateUpdated;
using MlkAdmin.Infrastructure.Providers.JsonProvider;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.Managers.Messages;
using MlkAdmin._2_Application.Managers.Embeds;
using MlkAdmin._3_Infrastructure.Cache;
using MlkAdmin._4_Presentation.Extensions;
using MlkAdmin._2_Application.Managers.Components;

namespace MlkAdmin.Presentation.DI
{
    public static class Registration
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddHostedService<DiscordBotHostService>();

            services.AddSingleton<DiscordEventsListener>();
            services.AddSingleton<CommandService>();
            services.AddSingleton(new DiscordSocketClient(new() { GatewayIntents = GatewayIntents.All}));

            services.AddJsonProvider<JsonDiscordChannelsMapProvider>("../../../3_Infrastructure/Configuration/DiscordChannelsMap.json");
            services.AddJsonProvider<JsonDiscordConfigurationProvider>("../../../3_Infrastructure/Configuration/DiscordConfiguration.json");
            services.AddJsonProvider<JsonDiscordEmotesProvider>("../../../3_Infrastructure/Configuration/DiscordEmotes.json");
            services.AddJsonProvider<JsonDiscordPicturesProvider>("../../../3_Infrastructure/Configuration/DiscordPictures.json");
            services.AddJsonProvider<JsonDiscordRolesProvider>("../../../3_Infrastructure/Configuration/DiscordRoles.json");
            services.AddJsonProvider<JsonDiscordCategoriesProvider>("../../../3_Infrastructure/Configuration/DiscordCategoriesMap.json");
            services.AddJsonProvider<JsonDiscordDynamicMessagesProvider>("../../../3_Infrastructure/Configuration/DiscordDynamicMessages.json");
            services.AddJsonProvider<JsonDiscordUsersLobbyProvider>("../../../3_Infrastructure/Configuration/DiscordUsersLobby.json");

            return services;
        }
        public static IServiceCollection AddInfastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ChannelsCache>();
            services.AddSingleton<RolesCache>();
            services.AddSingleton<EmotesCache>();
            services.AddSingleton<AutorizationCache>();
            services.AddSingleton<EmbedDescriptionsCache>();

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
            services.AddScoped<ComponentsManager>();

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IModeratorLogsSender, ModeratorLogsManager>();
            services.AddScoped<IDynamicMessageCenter, DynamicMessageManager>();
            services.AddScoped<IEmbedDtoCreator, EmbedManager>();

            return services;
        }
    }
}
