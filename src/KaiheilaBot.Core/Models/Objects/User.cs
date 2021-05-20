using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Objects
{
    public record User
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("identify_num")]
        public string IdentifyNumber { get; set; }

        [JsonPropertyName("online")]
        public bool Online { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        [JsonPropertyName("bot")]
        public bool Bot { get; set; }

        [JsonPropertyName("mobile_verified")]
        public bool MobileVerified { get; set; }

        [JsonPropertyName("system")]
        public bool System { get; set; }

        [JsonPropertyName("mobile_prefix")]
        public string MobilePrefix { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("invited_count")]
        public int InvitedCount { get; set; }

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        [JsonPropertyName("roles")]
        public IEnumerable<long> Roles { get; set; }
    }
}
