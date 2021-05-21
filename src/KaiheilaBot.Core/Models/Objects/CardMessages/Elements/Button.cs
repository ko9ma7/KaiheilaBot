using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Elements
{
    public record Button
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Button;

        [JsonPropertyName("theme")]
        public Themes Theme { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("click")]
        public string Click { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
