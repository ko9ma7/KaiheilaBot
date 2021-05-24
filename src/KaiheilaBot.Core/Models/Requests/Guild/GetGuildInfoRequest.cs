using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Guild
{
    public record GetGuildInfoRequest : BaseRequest
    {
        
        public override Method RequestMethod { get; init; } = Method.GET;
        
        public override string ResourcePath { get; init; } = "guild/view";

        [ParameterName("guild_id")]
        public string GuildId { get; set; }
    }
}
