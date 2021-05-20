using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.Common
{
    public record EmojiModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("name")] 
        public string Name { get; set; } 
    }
}
