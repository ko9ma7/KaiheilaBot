using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Events.Common;

namespace KaiheilaBot.Core.Models.Events.PrivateMessageEvents
{
    public record PrivateAddedReactionEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("emoji")]
        public EmojiModel Emoji { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("chat_code")]
        public string ChatCode { get; set; }

        [JsonPropertyName("msg_id")]
        public string MessageId { get; set; }
    }
}
