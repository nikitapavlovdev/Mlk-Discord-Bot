using System.ComponentModel.DataAnnotations;

namespace MlkAdmin._1_Domain.Entities
{
    public class VoiceChannel
    {
        [Key]
        public ulong Id { get; set; }
        public string? ChannelName { get; set; } = string.Empty;
        public bool IsGenerating { get; set; }
        public bool IsTemporary { get; set; }
        public string? Https { get; set; } = string.Empty;
        public string? Category {  get; set; } = string.Empty;
    }
}
