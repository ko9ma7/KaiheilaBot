using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.GuildMemberEvents
{
    public record ExitedGuildEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("exited_at")]
        public long ExitedAt { get; set; }
    }
}
