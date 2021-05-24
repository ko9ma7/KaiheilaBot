using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses.Gateway
{
    public record GatewayIndexResponse : IBaseResponseData
    {
        [JsonPropertyName("url")]
        public string Url { get; init; }
    }
}
