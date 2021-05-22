using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Events.GuildRoleEvents
{
    public class DeletedRoleEvent : IBaseEventExtraBody
    {
        [JsonPropertyName("role_id")]
        public long RoleId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("color")]
        public int Color { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("hoist")]
        public int Hoist { get; set; }

        [JsonPropertyName("mentionable")]
        public int Mentionable { get; set; }

        [JsonPropertyName("permissions")]
        public long Permissions { get; set; }
    }
}