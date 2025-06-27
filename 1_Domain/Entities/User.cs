using Discord.WebSocket;

namespace MlkAdmin._1_Domain.Entities
{
    public  class User
    {
        public int Id { get; set; }
        public ulong DiscordId { get; set; }
        public ulong? GuildId { get; set; }
        public string? Name { get; set; }
        public string? DiscordGlobalName { get; set; }
        public string? DiscordDisplayName { get; set; }
        public DateTime GuildJoinedAt { get; set; }
    }
}
