using MlkAdmin._3_Infrastructure.JsonModels.Categories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MlkAdmin._1_Domain.Interfaces.Discord;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordCategoriesProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordCategoriesProvider> logger;
        private readonly string filePath;
        private RootDiscordCategories? RootDiscordCategories { get; set; }

        #region Aliases
        public ulong AutoLobbyCategoryId => RootDiscordCategories.Guild.Autolobby.Id;
        #endregion

        public JsonDiscordCategoriesProvider(string filePath, ILogger<JsonDiscordCategoriesProvider> logger)
        {
            this.logger = logger;
            this.filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordCategories = JsonConvert.DeserializeObject<RootDiscordCategories>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}