using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Objects.Embedded
{
    public record ChannelPermissionOverwrites
    {
        [JsonPropertyName("role_id")]
        public int RoleId { get; set; }
        
        [JsonPropertyName("allow")]
        public int Allow { get; set; }
 
        [JsonPropertyName("deny")]
        public int Deny { get; set; }
    }
}
