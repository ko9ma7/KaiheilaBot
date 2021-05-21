using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Structures
{
    public record Paragraph : ISectionText
    {
        [JsonPropertyName("type")]
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Paragraph;
        
        [JsonPropertyName("cols")]
        public int Columns { get; set; }

        [JsonPropertyName("fields")]
        public IEnumerable<IParagraphField> Fields { get; set; }
    }
}
