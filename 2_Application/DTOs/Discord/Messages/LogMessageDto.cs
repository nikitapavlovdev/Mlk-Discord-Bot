namespace MlkAdmin._2_Application.DTOs.Discord.Messages
{
    public class LogMessageDto
    {
        public ulong UserId { get; set; }
        public ulong ChannelId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
