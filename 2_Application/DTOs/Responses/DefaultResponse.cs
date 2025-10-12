namespace MlkAdmin._2_Application.DTOs.Discord.Responses
{
    public class DefaultResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public Exception? Exception { get; set; } = null;
    }
}
