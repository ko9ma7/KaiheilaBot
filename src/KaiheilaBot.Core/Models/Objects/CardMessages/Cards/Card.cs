using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Cards
{
    public record Card
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Card;
        
        [JsonPropertyName("theme")]
        public Themes Theme { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("size")]
        public Sizes Size { get; init; }

        [JsonPropertyName("modules")]
        public IEnumerable<object> Modules { get; set; }
    }
}
