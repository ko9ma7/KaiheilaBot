using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses
{
    public record BaseResponse<T> where T : BaseResponseData
    {
        [JsonPropertyName("code")]
        public int Code { get; init; }
        
        [JsonPropertyName("message")]
        public string Message { get; init; }
        
        [JsonPropertyName("data")]
        public T Data { get; init; }
    }
}
