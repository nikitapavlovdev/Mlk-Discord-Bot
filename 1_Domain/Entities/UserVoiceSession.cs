using System.ComponentModel.DataAnnotations;

namespace MlkAdmin._1_Domain.Entities
{
    public class UserVoiceSession
    {
        [Key]
        public ulong UserId { get; set; }
        public DateTime? VoiceStarting { get; set; } = null;
        public long TotalSeconds { get; set; } = 0;
    }
}
