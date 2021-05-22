using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Elements;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Modules
{
    public record ImageGroup : IModuleBase
    {
        [JsonPropertyName("type")]
        public CardMessageTypes Type { get; init; } = CardMessageTypes.ImageGroup;

        [JsonPropertyName("elements")]
        public IEnumerable<Image> Elements { get; set; }
    }
}
