using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Elements
{
    public record KMarkdown : IParagraphField, IContextElement, ISectionText
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.KMarkdown;
        
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
