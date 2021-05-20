using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events
{
    /// <summary>
    /// Event -> "d" -> "extra"
    /// </summary>
    /// <typeparam name="T">Body Record</typeparam>
    public record BaseEventExtra<T>
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("body")]
        public T Body { get; set; }
    }
}
