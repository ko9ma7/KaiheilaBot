using System;
using KaiheilaBot.Interface;

namespace KaiheilaBot.Models {
    public class MessageEventArgs:EventArgs
    {
        public MessageEventArgs(ReceiveMessageData data, IConsole request)
        {
            Data = data;
            botRequest = request;
        }
        public ReceiveMessageData Data { get; set; }

        public IConsole botRequest { get; set; }
    }
}
