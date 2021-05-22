using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.GuildMemberEvents
{
    public record JoinedGuildEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("joined_at")]
        public long JoinedAt { get; set; }
    }
}
