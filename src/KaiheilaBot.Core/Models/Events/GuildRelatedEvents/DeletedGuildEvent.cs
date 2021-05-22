using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.GuildRelatedEvents
{
    public record DeletedGuildEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("notify_type")]
        public int NotifyType { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("enable_open")]
        public int EnableOpen { get; set; }

        [JsonPropertyName("open_id")]
        public long OpenId { get; set; }

        [JsonPropertyName("default_channel_id")]
        public string DefaultChannelId { get; set; }

        [JsonPropertyName("welcome_channel_id")]
        public string WelcomeChannelId { get; set; }
    }
}