using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Objects.Embedded
{
    // 好家伙统一一下 User 有这么难？ TMD 返回和文档还不一样
    public record UserInUserPermission
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("identify_num")]
        public string IdentifyNumber { get; set; }

        [JsonPropertyName("online")]
        public int Online { get; set; }

        [JsonPropertyName("os")]
        public string OperatingSystem { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        [JsonPropertyName("mobile_verified")]
        public bool MobileVerified { get; set; }

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        [JsonPropertyName("roles")]
        public IEnumerable<long> Roles { get; set; }

        [JsonPropertyName("joined_at")]
        public long JoinedAt { get; set; }

        [JsonPropertyName("active_time")]
        public long ActiveTime { get; set; }
    }
}
