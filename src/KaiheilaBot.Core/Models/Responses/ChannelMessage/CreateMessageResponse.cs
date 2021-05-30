using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses.ChannelMessage
{
    public record CreateMessageResponse : IBaseResponseData
    {
        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }

        [JsonPropertyName("msg_timestamp")]
        public long MessageTimestamp { get; set; }

        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
    }
}
