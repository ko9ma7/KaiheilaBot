using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses.Guild
{
    public record GetGuildListResponse : IBaseResponseData
    {
        [JsonPropertyName("items")]
        public IEnumerable<Objects.Guild> Items { get; set; }

        [JsonPropertyName("meta")]
        public PageMeta Meta { get; set; }
    }
}
