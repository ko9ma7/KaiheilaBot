using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Modules
{
    public record File : IModuleBase
    {
        [JsonPropertyName("type")]
        public CardMessageTypes Type { get; init; } = CardMessageTypes.File;

        [JsonPropertyName("src")]
        public string Source { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
