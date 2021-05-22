using System.Text.Json;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events
{
    public record BaseMessageEvent<T> where T : IBaseMessageEventDataExtra
    {
        /// <summary>
        /// 值永远是 0
        /// </summary>
        [JsonPropertyName("s")]
        public int EventType { get; set; }
        
        [JsonPropertyName("sn")]
        public long SerialNumber { get; set; }
        
        [JsonPropertyName("d")]
        public BaseMessageEventData<T> Data { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}