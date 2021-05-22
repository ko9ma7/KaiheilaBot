using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.UserRelatedEvents
{
    public record ExitedChannelEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("exited_at")]
        public long ExitedAt { get; set; }
    }
}