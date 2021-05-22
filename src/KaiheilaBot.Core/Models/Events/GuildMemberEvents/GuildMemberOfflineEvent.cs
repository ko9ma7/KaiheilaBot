using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.GuildMemberEvents
{
    public record GuildMemberOfflineEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("event_time")]
        public long EventTime { get; set; }

        [JsonPropertyName("guilds")]
        public IEnumerable<string> Guilds { get; set; }
    }
}