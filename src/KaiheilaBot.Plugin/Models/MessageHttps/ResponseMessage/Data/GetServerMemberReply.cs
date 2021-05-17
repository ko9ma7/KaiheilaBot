using System.Collections.Generic;

namespace KaiheilaBot
{
    public class GetServerMemberReply:BaseData
    {
        public IList<User> items { get; set; }
    }
}
