using RestSharp;

namespace KaiheilaBot.Core.Models.Requests
{
    public abstract record BaseRequest
    {
        public abstract Method RequestMethod { get; init; }

        public abstract string ResourcePath { get; init; }
    }
}
