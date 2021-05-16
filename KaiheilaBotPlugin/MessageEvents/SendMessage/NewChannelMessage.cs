namespace KaiheilaBot
{
    public class NewChannelMessage:AbstractMessageType
    {
        public NewChannelMessage(string server_guid, string name)
        {

        }

        public string guid_id { get; set; }
        public string parent_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int? limit_amount { get; set; }
        public int? voice_quality { get; set; }
    }
}
