using System.ComponentModel.DataAnnotations;

namespace MlkAdmin._1_Domain.Entities
{
    public  class User
    {
        [Key]
        public ulong Id { get; set; }
        public ulong GuildId { get; set; }
        public string? DiscordGlobalName { get; set; } = string.Empty;
        public string? DiscordDisplayName { get; set; } = string.Empty;
        public DateTime GuildJoinedAt { get; set; } = DateTime.MinValue;
    }
}