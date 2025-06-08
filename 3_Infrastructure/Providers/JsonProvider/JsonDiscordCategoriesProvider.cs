using MlkAdmin.Infrastructure.JsonModels.Categories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MlkAdmin._1_Domain.Interfaces;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordCategoriesProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordCategoriesProvider> _logger;
        private readonly string _filePath;
        public RootDiscordCategories? RootDiscordCategories { get; set; }

        public JsonDiscordCategoriesProvider(string filePath, ILogger<JsonDiscordCategoriesProvider> logger)
        {
            _logger = logger;
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordCategories = JsonConvert.DeserializeObject<RootDiscordCategories>(File.ReadAllText(_filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}