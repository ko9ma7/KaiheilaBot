using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.GuildRelatedEvents
{
    public class DeletedBlockListEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("operator_id")]
        public string OperatorId { get; set; }

        [JsonPropertyName("user_id")]
        public IEnumerable<string> UserId { get; set; }
    }
}