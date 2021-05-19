using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses.Gateway
{
    public record GatewayIndexResponse : BaseResponseData
    {
        [JsonPropertyName("url")]
        public string Url { get; init; }
    }
}
