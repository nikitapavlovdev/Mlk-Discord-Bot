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
using MlkAdmin._2_Application.Events.ButtonExecuted;
using MlkAdmin._2_Application.Events.GuildAvailable;
using MlkAdmin._2_Application.Events.Log;
using MlkAdmin._2_Application.Events.MessageReceived;
using MlkAdmin._2_Application.Events.ModalSubmitted;
using MlkAdmin._2_Application.Events.ReactionAdded;
using MlkAdmin._2_Application.Events.Ready;
using MlkAdmin._2_Application.Events.SelectMenuExecuted;
using MlkAdmin._2_Application.Events.UserJoined;
using MlkAdmin._2_Application.Events.UserLeft;
using MlkAdmin._2_Application.Events.UserVoiceStateUpdated;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._1_Domain.Interfaces.ModeratorsHelper;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.Managers.Messages;
using MlkAdmin._2_Application.Managers.Embeds;
using MlkAdmin._3_Infrastructure.Cache;
using MlkAdmin._4_Presentation.Extensions;
using MlkAdmin._2_Application.Managers.Components;
using MlkAdmin._2_Application.Events.UserUpdated;
using MlkAdmin._3_Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin._2_Application.Managers.Users;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannels;
using AniLiberty.NET.Client;

namespace MlkAdmin.Presentation.DI
{
    public static class Registration
    {
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
                typeof(ReactionAddedHandler).Assembly,
                typeof(GuildMemberUpdated).Assembly));


            services.AddScoped<RolesManager>();
            services.AddScoped<AutorizationManager>();
            services.AddScoped<VoiceChannelsManager>();
            services.AddScoped<TextChannelManager>();
            services.AddScoped<EmotesManager>();
            services.AddScoped<ModeratorLogsManager>();
            services.AddScoped<StaticDataServices>();
            services.AddScoped<EmbedMessageExtension>();
            services.AddScoped<SelectionMenuExtension>();
            services.AddScoped<MessageComponentsExtension>();
            services.AddScoped<ModalExtension>();
            services.AddScoped<ComponentsManager>();

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IModeratorLogsSender, ModeratorLogsManager>();
            services.AddScoped<IDynamicMessageCenter, DynamicMessageManager>();
            services.AddScoped<IEmbedDtoCreator, EmbedManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVoiceChannelRepository, VoiceChannelRepository>();
            services.AddScoped<UserSyncService>();
            services.AddScoped<VoiceChannelSyncServices>();

            return services;
        }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<RolesCache>();
            services.AddSingleton<EmotesCache>();
            services.AddSingleton<EmbedDescriptionsCache>();
            services.AddDbContext<MlkAdminDbContext>(options =>
            {
                options.UseSqlite("Data Source =D:\\Programming Life\\It\\Bots\\MlkBot\\AdminBot\\mlkadmin.db");
            });

            return services;
        }
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddHostedService<DiscordBotHostService>();

            services.AddScoped<DiscordEventsListener>();
            services.AddScoped<CommandService>();

            services.AddSingleton(new DiscordSocketClient(new() { GatewayIntents = GatewayIntents.All}));
            services.AddSingleton(new AnilibertyClient(new HttpClient()));

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
    }
}
