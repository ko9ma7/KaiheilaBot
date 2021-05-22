using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.UserRelatedEvents
{
    public record SelfJoinedGuildEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }
    }
}
