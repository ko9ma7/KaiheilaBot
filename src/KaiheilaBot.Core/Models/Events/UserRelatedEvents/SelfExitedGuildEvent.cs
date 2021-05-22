using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.UserRelatedEvents
{
    public record SelfExitedGuildEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }
    }
}