namespace MlkAdmin._2_Application.DTOs.Responses
{
    public class UserStatResponse
    {
        public long TotalSeconds { get; set; } = -1;
        public int MessageCount { get; set; } = -1;
        public bool IsSuccess { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }
}
