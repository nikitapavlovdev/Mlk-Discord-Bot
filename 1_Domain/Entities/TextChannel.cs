using System.ComponentModel.DataAnnotations;

namespace MlkAdmin._1_Domain.Entities
{
    public class TextChannel
    {
        [Key]
        public ulong Id { get; set; }
        public string? ChannelName { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
    }
}
