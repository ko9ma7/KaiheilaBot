using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Modules
{
    public record Context : IModuleBase, IParagraphField
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Context;

        [JsonPropertyName("elements")]
        public IEnumerable<IContextElement> Elements { get; set; }
    }
}
