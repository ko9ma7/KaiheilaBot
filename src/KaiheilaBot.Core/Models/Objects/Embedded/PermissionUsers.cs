using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Objects.Embedded
{
    public record PermissionUsers
    {
        [JsonPropertyName("user")]
        public UserInUserPermission User { get; set; }

        [JsonPropertyName("allow")]
        public int Allow { get; set; }

        [JsonPropertyName("deny")]
        public int Deny { get; set; }
    }
}
