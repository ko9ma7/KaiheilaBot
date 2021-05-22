using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.ChannelRelatedEvents
{
    public record DeletedChannelEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("deleted_at")]
        public long DeletedAt { get; set; }
    }
}