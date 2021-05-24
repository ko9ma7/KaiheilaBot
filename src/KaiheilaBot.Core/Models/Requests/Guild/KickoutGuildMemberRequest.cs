using System.Text.Json.Serialization;
using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Guild
{
    public record KickoutGuildMemberRequest : BaseRequest
    {
        public override Method RequestMethod { get; init; } = Method.POST;

        public override string ResourcePath { get; init; } = "guild/kickout";

        [ParameterName("guild_id")]
        public string GuildId { get; set; }

        [JsonPropertyName("target_id")]
        public string TargetId { get; set; }
    }
}
