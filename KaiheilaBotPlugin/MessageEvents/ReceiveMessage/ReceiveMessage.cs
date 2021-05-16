using Newtonsoft.Json;

namespace KaiheilaBot
{
    public class ReceiveMessage
    {
        [JsonProperty("s")]
        public int MessageType { get; set; }
        [JsonProperty("d")]
        public ReceiveMessageData Data { get; set; }
    }

    public class ReceiveMessageData
    {
        [JsonProperty("channel_type")]
        public string ChannelType { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("target_id")]
        public string TargetId { get; set; }
        [JsonProperty("author_id")]
        public string AuthorId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("msg_id")]
        public string MsgId { get; set; }
        [JsonProperty("msg_timestamp")]
        public long MsgTimestamp { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        public Extra Extra { get; set; }
    }
}
