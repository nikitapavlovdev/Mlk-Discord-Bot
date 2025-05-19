using MlkAdmin.Infrastructure.JsonModels.Categories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MlkAdmin.Core.Providers.JsonProvider
{
    public class JsonDiscordCategoriesProvider
    {
        public RootDiscordCategories? RootDiscordCategories { get; set; }

        public JsonDiscordCategoriesProvider(string filePath, ILogger<JsonDiscordCategoriesProvider> logger)
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