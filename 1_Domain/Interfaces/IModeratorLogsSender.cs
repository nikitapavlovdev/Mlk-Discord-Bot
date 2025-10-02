using MlkAdmin._2_Application.DTOs.Messages;

namespace MlkAdmin._1_Domain.Interfaces.ModeratorsHelper
{
    public interface IModeratorLogsSender
    {
        Task SendLogMessageAsync(LogMessageDto logMessage);
    }
}
