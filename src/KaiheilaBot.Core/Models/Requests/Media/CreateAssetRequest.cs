using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Media
{
    public record CreateAssetRequest : BaseRequest
    {
        public new Method RequestMethod { get; init; } = Method.POST;

        public new string ResourcePath { get; init; } = "asset/create";
        
        public string FilePath { get; set; }
    }
}
