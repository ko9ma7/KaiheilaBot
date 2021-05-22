using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.GuildMemberEvents
{
    public record UpdatedGuildMemberEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }
    }
}
