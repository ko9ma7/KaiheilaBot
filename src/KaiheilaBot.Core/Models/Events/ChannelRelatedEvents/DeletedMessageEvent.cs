using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.ChannelRelatedEvents
{
    public record DeletedMessageEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }
        
        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }
    }
}
