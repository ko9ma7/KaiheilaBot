using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.Embedded;

namespace KaiheilaBot.Core.Models.Objects
{
    public record Channel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }

        [JsonPropertyName("is_category")]
        public bool IsCategory { get; set; }

        [JsonPropertyName("parent_id")]
        public string ParentId { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("slow_mode")]
        public string SlowMode { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("permission_overwrites")]
        public IEnumerable<ChannelPermissionOverwrites> PermissionOverwrites { get; set; }
        
        [JsonPropertyName("permission_users")]
        public IEnumerable<User> PermissionUsers { get; set; }

        [JsonPropertyName("permission_sync")]
        public int PermissionSync { get; set; }
    }
}
