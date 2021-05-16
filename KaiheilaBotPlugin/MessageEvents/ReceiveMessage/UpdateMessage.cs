using Newtonsoft.Json;

namespace KaiheilaBot
{
    public class UpdateMessage : Extra
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("mention")]
        public object Mention { get; set; }
        [JsonProperty("mention_all")]
        public bool MentionAll { get; set; }
        [JsonProperty("mention_here")]
        public bool MentionHere { get; set; }
        [JsonProperty("mention_roles")]
        public object MentionRoles { get; set; }
        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
    }
}
