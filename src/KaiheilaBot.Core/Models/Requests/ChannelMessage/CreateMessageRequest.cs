using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.ChannelMessage
{
    public record CreateMessageRequest : BaseRequest
    {
        public override Method RequestMethod { get; init; } = Method.POST;
        public override string ResourcePath { get; init; } = "message/create";

        [ParameterName("type")] 
        public int MessageType { get; set; } = 1;

        [ParameterName("target_id")]
        public string ChannelId { get; set; }
        
        [ParameterName("content")] 
        public string Content { get; set; }

        [ParameterName("quote")] 
        public string Quote { get; set; } = "";

        [ParameterName("temp_target_id")]
        public string TempTargetId { get; set; } = "";

        [ParameterName("nonce")]
        public string Nonce { get; set; } = "";
    }
}
