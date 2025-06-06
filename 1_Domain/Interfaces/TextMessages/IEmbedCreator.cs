using MlkAdmin._1_Domain.Enums;
using MlkAdmin._2_Application.DTOs;

namespace MlkAdmin._1_Domain.Interfaces.TextMessages
{
    public interface IEmbedCreator
    {
        Task<EmbedDto> CreateEmbed(DynamicMessageType type);
    }
}
