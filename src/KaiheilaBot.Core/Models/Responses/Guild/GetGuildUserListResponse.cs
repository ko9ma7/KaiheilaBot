using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses.Guild
{
    public record GetGuildUserListResponse : IBaseResponseData
    {
        [JsonPropertyName("items")]
        public IEnumerable<Objects.User> Users { get; set; }

        [JsonPropertyName("user_count")]
        public int UserCount { get; set; }
        
        [JsonPropertyName("online_count")]
        public int OnlineCount { get; set; }
        
        [JsonPropertyName("offline_count")]
        public int OfflineCount { get; set; } 
    }
}
