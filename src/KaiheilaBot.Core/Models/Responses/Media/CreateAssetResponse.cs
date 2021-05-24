using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses.Media
{
    public record CreateAssetResponse : IBaseResponseData
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
