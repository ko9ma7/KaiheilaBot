using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Objects.Embedded
{
    // TMD 这个 API 给爷整麻了
    public record ChannelListInGuild
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("master_id")]
        public string MasterId { get; set; }

        [JsonPropertyName("parent_id")]
        public string ParentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("limit_amount")]
        public int LimitAmount { get; set; }

        [JsonPropertyName("is_category")]
        public bool IsCategory { get; set; }
    }
}
