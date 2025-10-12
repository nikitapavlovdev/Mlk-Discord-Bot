using MlkAdmin._1_Domain.Enums;
using MlkAdmin._2_Application.DTOs.Discord.Embed;

namespace MlkAdmin._1_Domain.Interfaces.Discord
{
    public interface IEmbedDtoCreator
    {
        Task<EmbedDto> GetEmbedDto(DynamicMessageType type);
    }
}