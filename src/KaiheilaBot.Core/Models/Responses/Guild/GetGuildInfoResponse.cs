using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects;
using KaiheilaBot.Core.Models.Objects.Embedded;

namespace KaiheilaBot.Core.Models.Responses.Guild
{
    public record GetGuildInfoResponse : IBaseResponseData
    {        
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("master_id")]
        public string MasterId { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("notify_type")]
        public int NotifyType { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("enable_open")]
        public bool EnableOpen { get; set; }

        [JsonPropertyName("open_id")]
        public string OpenId { get; set; }

        [JsonPropertyName("default_channel_id")]
        public string DefaultChannelId { get; set; }

        [JsonPropertyName("welcome_channel_id")]
        public string WelcomeChannelId { get; set; }

        [JsonPropertyName("roles")]
        public IEnumerable<Role> Roles { get; set; }

        [JsonPropertyName("channels")]
        public IEnumerable<ChannelListInGuild> Channels { get; set; }
    }
}