using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.ChannelRelatedEvents
{
    public record UpdatedMessageEvent : BaseEventExtraBody
    {
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("mention")]
        public IEnumerable<string> Mention { get; set; }
        
        [JsonPropertyName("mention_all")]
        public bool MentionAll { get; set; }
        
        [JsonPropertyName("mention_here")]
        public bool MentionHere { get; set; }

        [JsonPropertyName("mention_roles")]
        public IEnumerable<int> MentionRoles { get; set; }
        
        [JsonPropertyName("updated_at")]
        public long UpdatedAt { get; set; }
        
        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }
    }
}
