using System;
using System.Collections.Generic;
using System.Text;

namespace KaiheilaBot
{
    public class GetServerMemberMessage
    {
        public GetServerMemberMessage(string guid)
        {
            guid_id = guid_id;
        }
        public string guid_id { get; set; }
        public string channel_id { get; set; }
        public string search { get; set; }
        public int role_id { get; set; }
        public int mobile_verified { get; set; }
        public int active_time { get; set; }
        public int joined_at { get; set; }
        public int page { get; set; }
        public int page_size { get; set; }
    }
}
