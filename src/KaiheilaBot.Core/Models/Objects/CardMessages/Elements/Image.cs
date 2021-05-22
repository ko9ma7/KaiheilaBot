using System.Drawing;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Elements
{
    public record Image : IContextElement, ISectionAccessory
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Image;

        [JsonPropertyName("src")]
        public string Source { get; set; }

        [JsonPropertyName("alt")]
        public string Alter { get; set; }

        [JsonPropertyName("size")]
        public Sizes Size { get; set; }

        [JsonPropertyName("circle")]
        public bool Circle { get; set; }

        public Image(string source, string alter = "", Sizes size = Sizes.Lg, bool circle = false)
        {
            Source = source;
            Alter = alter;
            Size = size;
            Circle = circle;
        }
    }
}
