using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.DTOs;
using MlkAdmin._3_Infrastructure.Cache;
using MlkAdmin.Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Managers.Embeds
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
                    Title = "ᴀᴜᴛᴏʀɪᴢᴀᴛɪᴏɴ",
                    Description = embedDescriptionsCache.GetDescriptionForAutorization(),
                    PicturesUrl = jsonDiscordPicturesProvider.RootDiscordPictures.Pinterest.ForMessage.AuMessage
                },

                DynamicMessageType.Features => new EmbedDto()
                {
                    Title = "ꜰᴇᴀᴛᴜʀᴇs",
                    Description = embedDescriptionsCache.GetDescriptionForFeatures(),
                    PicturesUrl = jsonDiscordPicturesProvider.RootDiscordPictures.Pinterest.ForMessage.AutoLobbyNamingMessage
                },

                DynamicMessageType.Rules => new EmbedDto()
                {
                    Title = "ᴍᴀʟᴇɴᴋɪᴇ ʀᴜʟᴇs",
                    Description = embedDescriptionsCache.GetDescriptionForRules(),
                    PicturesUrl = jsonDiscordPicturesProvider.RootDiscordPictures.Pinterest.ForMessage.RulesBanner
                },

                DynamicMessageType.Roles => new EmbedDto()
                {
                    Title = "ᴍᴀʟᴇɴᴋɪᴇ ʀᴏʟᴇs",
                    Description = embedDescriptionsCache.GetDiscriptionForMainRoles(), 
                    PicturesUrl = ""
                },

                DynamicMessageType.NameColor => new EmbedDto()
                {
                    Title = "ɴɪᴄᴋɴᴀᴍᴇ ᴄᴏʟᴏʀ",
                    Description = embedDescriptionsCache.GetDescriptionForNameColor(),
                    PicturesUrl = ""
                },

                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown type: {type}")
            });
        }
    }
}
