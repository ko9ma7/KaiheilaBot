using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Modules
{
    public record Countdown : IModuleBase
    {
        [JsonPropertyName("type")]
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Countdown;

        [JsonPropertyName("endTime")]
        public long EndTime { get; set; }

        [JsonPropertyName("startTime")]
        public long StartTime { get; set; }

        [JsonPropertyName("mode")]
        public CountdownModes Mode { get; set; }
    }
}
