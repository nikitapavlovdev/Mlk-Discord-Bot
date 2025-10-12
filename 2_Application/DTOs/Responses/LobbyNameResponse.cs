namespace MlkAdmin._2_Application.DTOs.Discord.Responses
{
    public class LobbyNameResponse
    {
        public string? LobbyName { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message {  get; set; } = string.Empty;
        public string Error {  get; set; } = string.Empty;
    }
}
