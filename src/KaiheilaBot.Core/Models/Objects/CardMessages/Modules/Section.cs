using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Modules
{
    public record Section : IModuleBase
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Section;

        [JsonPropertyName("mode")]
        public SectionModes Mode { get; set; }

        [JsonPropertyName("text")]
        public IEnumerable<ISectionText> Text { get; set; }

        [JsonPropertyName("accessory")]
        public IEnumerable<ISectionAccessory> Accessory { get; set; }
    }
}
