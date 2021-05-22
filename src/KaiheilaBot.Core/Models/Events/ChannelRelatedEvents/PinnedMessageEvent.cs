using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.ChannelRelatedEvents
{
    public record PinnedMessageEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("operator_id")]
        public string OperatorId { get; set; }

        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }
    }
}
