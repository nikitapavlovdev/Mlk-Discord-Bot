using Discord;

namespace MlkAdmin._2_Application.DTOs.Embed
{
    public class EmbedDto
    {
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? PicturesUrl { get; set; } = string.Empty;

        public bool WithTimestamp { get; set; } = false;

        public Color Color { get; set; } = new(50, 50, 53);
        public FooterDto? FooterDto { get; set; }
        public AuthorDto? AuthorDto { get; set; }

        public IEnumerable<EmbedFieldBuilder>? EmbedFields { get; set; } 
    }
}
