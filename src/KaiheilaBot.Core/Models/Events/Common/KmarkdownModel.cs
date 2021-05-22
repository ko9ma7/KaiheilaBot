using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.Common
{
    public record KmarkdownModel
    {
        [JsonPropertyName("raw_content")]
        public string RawContent { get; set; }

        [JsonPropertyName("mention_part")]
        public IEnumerable<string> MentionPart { get; set; }

        [JsonPropertyName("mention_role_part")]
        public IEnumerable<string> MentionRolePart { get; set; }
    }
}
