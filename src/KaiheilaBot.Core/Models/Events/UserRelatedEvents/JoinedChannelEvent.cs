using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.UserRelatedEvents
{
    public record JoinedChannelEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("joined_at")]
        public long JoinedAt { get; set; }
    }
}
