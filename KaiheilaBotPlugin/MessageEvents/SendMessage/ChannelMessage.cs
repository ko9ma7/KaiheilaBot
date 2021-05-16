
namespace KaiheilaBot
{
    public class ChannelMessage:AbstractMessageType
    {
        public ChannelMessage(ReceiveMessageData request, string content, bool quote = true, bool tempMessage = false)
        {
            Type = request.Type;
            Target_Id = request.TargetId;
            Content = content;
            if (quote)
            {
                Quote = request.MsgId;
            }
            Nonce = request.Nonce;
            if (tempMessage)
            {
                Temp_Target_Id = (request.Extra as ExtraText).Author.Id;
            }
        }

        public int Type { get; set; }

        public string Target_Id { get; set; }

        public string Content { get; set; }

        public string Quote { get; set; }

        public string Nonce { get; set; }

        public string Temp_Target_Id { get; set; }
    }
}
