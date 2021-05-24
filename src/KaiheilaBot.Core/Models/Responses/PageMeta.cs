using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses
{
    public record PageMeta
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
    
        [JsonPropertyName("page_total")]
        public int PageTotal { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
