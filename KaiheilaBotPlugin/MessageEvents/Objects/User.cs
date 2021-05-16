using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KaiheilaBot
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("identify_num")]
        public string IdentifyNum { get; set; }
        [JsonProperty("online")]
        public bool IsOnline { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("bot")]
        public bool IsBot { get; set; }
        [JsonProperty("mobile_verified")]
        public bool IsMobileVerified { get; set; }
        [JsonProperty("system")]
        public bool IsSystem { get; set; }
        [JsonProperty("mobile_prefix")]
        public string MobilePrefix { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [JsonProperty("invited_count")]
        public int InvitedCount { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        public List<int> roles { get; set; }
    }
}
