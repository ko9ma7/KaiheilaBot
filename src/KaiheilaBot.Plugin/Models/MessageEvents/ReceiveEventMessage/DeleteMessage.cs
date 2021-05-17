using Newtonsoft.Json;

namespace KaiheilaBot
{
    public class DeleteMessage : Extra
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
    }
}
