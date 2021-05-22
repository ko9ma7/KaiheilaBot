using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Events.Common;
using KaiheilaBot.Core.Models.Objects;

namespace KaiheilaBot.Core.Models.Events.MessageRelatedEvents
{
    public record VideoMessageEvent : IBaseMessageEventDataExtra
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("attachments")]
        public AttachmentModel Attachment { get; set; }

        [JsonPropertyName("author")]
        public User Author { get; set; }
    }
}
