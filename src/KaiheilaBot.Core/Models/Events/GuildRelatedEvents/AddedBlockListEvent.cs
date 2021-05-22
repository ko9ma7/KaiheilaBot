using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.GuildRelatedEvents
{
    public record AddedBlockListEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("operator_id")]
        public string OperatorId { get; set; }

        [JsonPropertyName("remark")]
        public string Remark { get; set; }

        [JsonPropertyName("user_id")]
        public IEnumerable<string> UserId { get; set; }
    }
}
