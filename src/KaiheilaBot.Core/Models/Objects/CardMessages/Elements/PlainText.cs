using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Elements
{
    public record PlainText : IParagraphField, IContextElement, ISectionText
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.PlainText;

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("emoji")]
        public bool Emoji { get; set; }
    }
}
