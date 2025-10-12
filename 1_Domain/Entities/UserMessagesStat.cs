using System.ComponentModel.DataAnnotations;

namespace MlkAdmin._1_Domain.Entities
{
    public class UserMessagesStat
    {
        [Key]
        public ulong Id { get; set; }
        public int Count { get; set; } = 0;
        public DateTime? LastUpdate { get; set; } = DateTime.UtcNow;
    }
}
