using Easy.MessageHub;
using RestSharp;

namespace KaiheilaBot
{
    public static class Globals
    {
        public static RestClient RestClient { get; set; }
        
        public static MessageHub MessageHub { get; set; }
    }
}
