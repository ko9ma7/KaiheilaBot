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

        /// <summary>
        /// object 类型便于序列化，只接受 Paragraph, Kmarkdown, PlainText => ISectionText
        /// </summary>
        [JsonPropertyName("text")]
        public object Text { get; set; }

        /// <summary>
        /// object 类型便于序列化，只接受 Image, Button => ISectionAccessory
        /// </summary>
        [JsonPropertyName("accessory")]
        public object Accessory { get; set; }
    }
}
