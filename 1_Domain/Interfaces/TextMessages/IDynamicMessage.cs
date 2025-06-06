using MlkAdmin._2_Application.DTOs;

namespace MlkAdmin._1_Domain.Interfaces.TextMessages
{
    public interface IMessageUpdater
    {
        Task SendOrUpdateAsync(DynamicMessageDto dto);
    }
}
