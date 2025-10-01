using Discord;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.DTOs;

namespace MlkAdmin._3_Infrastructure.Discord.Extensions
{
    public class EmbedMessageExtension(ILogger<EmbedMessageExtension> logger)
    {
        public Embed CreateEmbed(EmbedDto embedDto) 
        {
            try
            {
                EmbedBuilder builder = new EmbedBuilder()
                    .WithTitle(embedDto.Title)
                    .WithDescription(embedDto.Description)
                    .WithColor(embedDto.Color);

                if (embedDto.AuthorDto is not null)
                {
                    builder.WithAuthor(new EmbedAuthorBuilder()
                        .WithUrl(embedDto.AuthorDto.Url)
                        .WithName(embedDto.AuthorDto.Name)
                        .WithIconUrl(embedDto.AuthorDto.IconUrl));
                }

                if (embedDto.FooterDto is not null)
                {
                    builder.WithFooter(new EmbedFooterBuilder()
                        .WithText(embedDto.FooterDto.Text)
                        .WithIconUrl(embedDto.FooterDto.IconUrl));
                }

                if (embedDto.WithTimestamp) 
                { 
                    builder.WithCurrentTimestamp(); 
                }

                if(embedDto.EmbedFields is not null)
                {
                    builder.WithFields(embedDto.EmbedFields);
                }

                return builder.Build();
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);

                return new EmbedBuilder().Build();
            }
        }
    }
};