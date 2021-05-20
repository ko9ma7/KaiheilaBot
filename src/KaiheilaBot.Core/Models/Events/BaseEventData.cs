using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events
{
    /// <summary>
    /// Event -> "d"
    /// </summary>
    /// <typeparam name="T">Body Record</typeparam>
    public record BaseEventData<T> where T : BaseEventExtraBody
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
        public BaseEventExtra<T> Extra { get; set; }
    }
}