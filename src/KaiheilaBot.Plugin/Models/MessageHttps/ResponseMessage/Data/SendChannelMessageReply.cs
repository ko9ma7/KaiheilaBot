using System;
using System.Collections.Generic;
using System.Text;

namespace KaiheilaBot
{
    public class SendChannelMessageReply:BaseData
    {
        public string msg_id { get; set; }
        public long msg_timestamp { get; set; }
        public string nonce { get; set; }
    }
}
