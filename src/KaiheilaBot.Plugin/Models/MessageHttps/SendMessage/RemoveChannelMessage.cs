﻿namespace KaiheilaBot
{
    public class RemoveChannelMessage:AbstractMessageType
    {
        public RemoveChannelMessage(string channelId)
        {
            channel_id = channelId;
        }
        public string channel_id { get; set; }
    }
}
