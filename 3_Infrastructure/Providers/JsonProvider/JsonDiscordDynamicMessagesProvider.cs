using MlkAdmin._3_Infrastructure.JsonModels.DynamicMessages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MlkAdmin._1_Domain.Interfaces;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordDynamicMessagesProvider : IJsonConfigurationProvider
    {

        private readonly ILogger<JsonDiscordDynamicMessagesProvider> logger;
        private readonly string filePath;
        private RootDynamicMessages? DynamicMessages { get; set; }

        #region Aliases
        public ulong AuMessageId => DynamicMessages.Messages.ServerHub.Autorization.Id;
        public ulong FeaturesMessageId => DynamicMessages.Messages.ServerHub.Features.Id;
        public ulong RulesMessageId => DynamicMessages.Messages.Rules.Id;
        public ulong MainRolesMessageId => DynamicMessages.Messages.Roles.Main.Id;
        public ulong NameColorRolesMessageId => DynamicMessages.Messages.Roles.NameColor.Id;
        #endregion

        public JsonDiscordDynamicMessagesProvider(string filePath, ILogger<JsonDiscordDynamicMessagesProvider> logger)
        {
            this.logger = logger;
            this.filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                DynamicMessages = JsonConvert.DeserializeObject<RootDynamicMessages>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
