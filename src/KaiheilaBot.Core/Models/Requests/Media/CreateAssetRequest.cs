using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Media
{
    public record CreateAssetRequest : BaseRequest
    {
        public override Method RequestMethod { get; init; } = Method.POST;

        public override string ResourcePath { get; init; } = "asset/create";
        
        public string FilePath { get; set; }
    }
}
