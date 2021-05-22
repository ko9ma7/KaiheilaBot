using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.UserRelatedEvents
{
    public record MessageButtonClickEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("target_id")]
        public string TargetId { get; set; }
    }
}
