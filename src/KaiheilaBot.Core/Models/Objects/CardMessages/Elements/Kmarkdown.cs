using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Elements
{
    public record Kmarkdown : IParagraphField, IContextElement, ISectionText
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Kmarkdown;
        
        [JsonPropertyName("content")]
        public string Content { get; set; }

        public Kmarkdown(string content)
        {
            Content = content;
        }
    }
}
