
using KaiheilaBot.Interface;

namespace KaiheilaBot.Models
{
    public class TextMessageEventArgs : MessageEventArgs
    {
        public TextMessageEventArgs(ReceiveMessageData data, IConsole request) : base(data, request)
        {
        }
        public string Content
        {
            get
            {
                return Data.Content;
            }
        }
    }
}
