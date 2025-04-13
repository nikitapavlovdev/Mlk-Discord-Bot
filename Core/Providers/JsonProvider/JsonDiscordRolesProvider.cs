using Discord_Bot.Infrastructure.JsonModels.Channels;
using Discord_Bot.Infrastructure.JsonModels.Roles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Discord_Bot.Core.Providers.JsonProvider
{
    public class JsonDiscordRolesProvider
    {
        public RootDiscordRoles? RootDiscordRoles { get; set; }

        public JsonDiscordRolesProvider(string filePath, ILogger<JsonDiscordRolesProvider> logger)
        {
            try
            {
                RootDiscordRoles = JsonConvert.DeserializeObject<RootDiscordRoles>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
