using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Modules
{
    public record Context : IModuleBase, IParagraphField
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Context;

        /// <summary>
        /// object 类型便于序列化，只接受 Image, Kmarkdown, PlainText => IContextElement
        /// </summary>
        [JsonPropertyName("elements")]
        public IEnumerable<object> Elements { get; set; }
    }
}
