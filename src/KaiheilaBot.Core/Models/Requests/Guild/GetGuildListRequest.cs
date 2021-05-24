using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Guild
{
    public record GetGuildListRequest : BaseRequestWithPage
    {
        public override Method RequestMethod { get; init; } = Method.GET;

        public override string ResourcePath { get; init; } = "guild/list";

        public override int Page { get; set; } = 1;
        
        public override int PageSize { get; set; } = 100;
        
        public override string Sort { get; set; } = "";
    }
}
