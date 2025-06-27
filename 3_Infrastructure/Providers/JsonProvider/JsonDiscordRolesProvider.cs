using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin.Infrastructure.JsonModels.Roles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MlkAdmin.Infrastructure.JsonModels.Pictures;

namespace MlkAdmin.Infrastructure.Providers.JsonProvider
{
    public class JsonDiscordRolesProvider : IJsonConfigurationProvider
    {
        private readonly ILogger<JsonDiscordRolesProvider> _logger;
        private readonly string _filePath;
        public RootDiscordRoles? RootDiscordRoles { get; set; }

        public JsonDiscordRolesProvider(string filePath, ILogger<JsonDiscordRolesProvider> logger)
        {
            _logger = logger;
            _filePath = filePath;
            Load();
        }

        public void Load()
        {
            try
            {
                RootDiscordRoles = JsonConvert.DeserializeObject<RootDiscordRoles>(File.ReadAllText(_filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}