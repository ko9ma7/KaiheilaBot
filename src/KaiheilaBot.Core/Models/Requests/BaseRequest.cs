using RestSharp;

namespace KaiheilaBot.Core.Models.Requests
{
    public record BaseRequest
    {
        public Method RequestMethod { get; init; }

        public string ResourcePath { get; init; }
    }
}
