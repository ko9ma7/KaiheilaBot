

namespace KaiheilaBot
{
    public class RemoveMessageMessage : AbstractMessageType
    {
        public RemoveMessageMessage(string msg_id)
        {
            this.msg_id = msg_id;
        }
        public string msg_id { get; set; }
    }
}
