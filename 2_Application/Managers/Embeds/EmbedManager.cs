
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.DTOs;

namespace MlkAdmin._2_Application.Managers.Embeds
{
    public class EmbedManager : IEmbedCreator
    {
        public Task<EmbedDto> CreateEmbed(DynamicMessageType type)
        {
            return Task.FromResult(type switch
            {
                DynamicMessageType.Rules => new EmbedDto()
                {
                    Title = "Rule Test",
                    Description = "Rule dicription"
                },

                DynamicMessageType.Roles => new EmbedDto()
                {
                    Title = "Role Test",
                    Description = "Role dicription"
                },

                DynamicMessageType.NameColor => new EmbedDto()
                {
                    Title = "NameColor Test",
                    Description = "NameColor dicription"
                },

                DynamicMessageType.AuthorizationCheck => new EmbedDto()
                {
                    Title = "AuthorizationCheck Test",
                    Description = "AuthorizationCheck dicription"
                },

                DynamicMessageType.Features => new EmbedDto()
                {
                    Title = "Features Test",
                    Description = "Features dicription"
                },

                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown type: {type}")
            });
        }
    }
}
