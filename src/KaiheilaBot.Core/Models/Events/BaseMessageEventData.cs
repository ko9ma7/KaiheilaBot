using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events
{
    public record BaseMessageEventData<T> where T : IBaseMessageEventDataExtra
    {
        [JsonPropertyName("channel_type")]
        public string ChannelType { get; set; }
        
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("target_id")]
        public string TargetId { get; set; }

        [JsonPropertyName("author_id")]
        public string AuthorId { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }
        
        [JsonPropertyName("msg_timestamp")]
        public long MessageTimestamp { get; set; }
        
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
        
        [JsonPropertyName("extra")]
        public T Extra { get; set; }
    }
}