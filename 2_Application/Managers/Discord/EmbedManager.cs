using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._2_Application.DTOs.Discord.Embed;
using MlkAdmin._3_Infrastructure.Cache;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Managers.Discord
{
    public class EmbedManager(
        EmbedDescriptionsCache embedDescriptionsCache,
        JsonDiscordPicturesProvider jsonDiscordPicturesProvider) : IEmbedDtoCreator
    {
        public Task<EmbedDto> GetEmbedDto(DynamicMessageType type)
        {
            return Task.FromResult(type switch
            {
                DynamicMessageType.Authorization => new EmbedDto()
                {
                    Title = "Авторизация",
                    Description = embedDescriptionsCache.GetDescriptionForAutorization(),
                    PicturesUrl = jsonDiscordPicturesProvider.PinterestPictureForAuMessageLink
                },

                DynamicMessageType.Rules => new EmbedDto()
                {
                    Title = "Правила",
                    Description = embedDescriptionsCache.GetDescriptionForRules(),
                    PicturesUrl = jsonDiscordPicturesProvider.PinterestPictureForRulesLink
                },

                DynamicMessageType.Roles => new EmbedDto()
                {
                    Title = "Роли",
                    //Description = embedDescriptionsCache.GetDiscriptionForMainRoles(), 
                    Description = embedDescriptionsCache.GetDiscriptionForMainRolesNewVers()
                },

                DynamicMessageType.NameColor => new EmbedDto()
                {
                    Title = "Цвет имени",
                    Description = embedDescriptionsCache.GetDescriptionForNameColor(),
                },

                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown type: {type}")
            });
        }
    }
}