using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.PrivateMessageEvents
{
    public record DeletedPrivateMessageEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("chat_code")]
        public string ChatCode { get; set; }

        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }

        [JsonPropertyName("author_id")]
        public string AuthorId { get; set; }

        [JsonPropertyName("target_id")]
        public string TargetId { get; set; }

        [JsonPropertyName("deleted_at")]
        public long DeletedAt { get; set; }
    }
}
