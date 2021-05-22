using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Events.Common;
using KaiheilaBot.Core.Models.Objects;

namespace KaiheilaBot.Core.Models.Events.MessageRelatedEvents
{
    public record KmarkdownMessageEvent : IBaseMessageEventDataExtra
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }

        [JsonPropertyName("channel_name")]
        public string ChannelName { get; set; }

        [JsonPropertyName("mention")]
        public IEnumerable<string> Mention { get; set; }

        [JsonPropertyName("mention_all")]
        public bool MentionAll { get; set; }

        [JsonPropertyName("mention_roles")]
        public IEnumerable<long> MentionRoles { get; set; }

        [JsonPropertyName("mention_here")]
        public bool MentionHere { get; set; }

        [JsonPropertyName("nav_channels")]
        public IEnumerable<string> NavChannels { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("author")]
        public User Author { get; set; }

        [JsonPropertyName("kmarkdown")]
        public KmarkdownModel Kmarkdown { get; set; }
    }
}