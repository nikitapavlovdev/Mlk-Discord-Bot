using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.TextMessages;
using MlkAdmin._2_Application.DTOs;
using MlkAdmin._3_Infrastructure.Cache;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

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
                    PicturesUrl = jsonDiscordPicturesProvider.PinterestPictureForAuMessageLink
                },

                DynamicMessageType.Features => new EmbedDto()
                {
                    Title = "ꜰᴇᴀᴛᴜʀᴇs",
                    Description = embedDescriptionsCache.GetDescriptionForFeatures(),
                    PicturesUrl = jsonDiscordPicturesProvider.PinterestPictureForAutoLobbyNamingMessageLink
                },

                DynamicMessageType.Rules => new EmbedDto()
                {
                    Title = "ᴍᴀʟᴇɴᴋɪᴇ ʀᴜʟᴇs",
                    Description = embedDescriptionsCache.GetDescriptionForRules(),
                    PicturesUrl = jsonDiscordPicturesProvider.PinterestPictureForRulesLink
                },

                DynamicMessageType.Roles => new EmbedDto()
                {
                    Title = "ᴍᴀʟᴇɴᴋɪᴇ ʀᴏʟᴇs",
                    Description = embedDescriptionsCache.GetDiscriptionForMainRoles(), 
                },

                DynamicMessageType.NameColor => new EmbedDto()
                {
                    Title = "ɴɪᴄᴋɴᴀᴍᴇ ᴄᴏʟᴏʀ",
                    Description = embedDescriptionsCache.GetDescriptionForNameColor(),
                },

                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown type: {type}")
            });
        }
        public Task<EmbedDto> GetEmbedDto(StaticMessageType type)
        {
            return Task.FromResult(type switch
            {
                StaticMessageType.ServerPeculiarities => new EmbedDto()
                {
                    Title = "ᴘᴇᴄᴜʟɪᴀʀɪᴛɪᴇs",
                    Description = embedDescriptionsCache.GetDescriptionForServerPeculiarities(),
                    PicturesUrl = jsonDiscordPicturesProvider.PinterestPictureForServerPeculiaritiesImg
                },

                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown type: {type}")
            });
        }
    }
}