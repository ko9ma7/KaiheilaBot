using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Events.Common;

namespace KaiheilaBot.Core.Models.Events.ChannelRelatedEvents
{
    public record AddedReactionEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }
        
        [JsonPropertyName("emoji")]
        public EmojiModel Emoji { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
        
        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }
    }
}
