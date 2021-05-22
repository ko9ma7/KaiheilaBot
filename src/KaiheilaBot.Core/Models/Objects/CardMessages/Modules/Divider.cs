using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Modules
{
    public record Divider : IModuleBase
    {
        [JsonPropertyName("type")] 
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Divider;
    }
}
