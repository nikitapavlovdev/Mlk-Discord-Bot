using MlkAdmin._1_Domain.Enums;
using MlkAdmin._2_Application.DTOs.Embed;

namespace MlkAdmin._1_Domain.Interfaces.Discord
{
    public interface IEmbedDtoCreator
    {
        Task<EmbedDto> GetEmbedDto(DynamicMessageType type);
        Task<EmbedDto> GetEmbedDto(StaticMessageType type);
    }
}