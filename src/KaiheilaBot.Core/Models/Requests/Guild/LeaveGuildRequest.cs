using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Guild
{
    public record LeaveGuildRequest : BaseRequest
    {
        public override Method RequestMethod { get; init; } = Method.POST;

        public override string ResourcePath { get; init; } = "guild/leave";

        [ParameterName("guild_id")]
        public string GuildId { get; set; }
    }
}