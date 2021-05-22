using System.Text.Json;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events
{
    /// <summary>
    /// "s": 0 的 Event Models
    /// </summary>
    /// <typeparam name="T">Body Record</typeparam>
    public record BaseEvent<T> where T : IBaseEventExtraBody
    {
        /// <summary>
        /// 值永远是 0
        /// </summary>
        [JsonPropertyName("s")]
        public int EventType { get; set; }
        
        [JsonPropertyName("sn")]
        public long SerialNumber { get; set; }
        
        [JsonPropertyName("d")]
        public BaseEventData<T> Data { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
