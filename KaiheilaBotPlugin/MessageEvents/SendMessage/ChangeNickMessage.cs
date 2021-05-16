
namespace KaiheilaBot
{
    public class ChangeNickMessage:AbstractMessageType
    {
        public ChangeNickMessage(string guid, string newnick, string user_id)
        {
            guid_id = guid;
            nickname = newnick;
            this.user_id = user_id;
        }
        public string guid_id { get; set; }
        public string nickname { get; set; }
        public string user_id { get; set; }
    }
}
