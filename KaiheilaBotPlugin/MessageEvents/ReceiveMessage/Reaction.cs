using Newtonsoft.Json;

namespace KaiheilaBot
{
    public class Reaction : Extra
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
    }

    public class Emoji
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
