using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.PrivateMessageEvents
{
    public record UpdatedPrivateMessageEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("author_id")]
        public string AuthorId { get; set; }

        [JsonPropertyName("target_id")]
        public string TargetId { get; set; }

        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("updated_at")]
        public long UpdatedAt { get; set; }
        
        [JsonPropertyName("chat_code")]
        public string ChatCode { get; set; }
    }
}