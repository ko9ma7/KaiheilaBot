using KaiheilaBot;
using System;
using System.Collections.Generic;
using System.Text;

namespace KaiheilaBot
{
    public class GetChannelInfoReply:BaseData
    {
        public string id { get; set; }
        public string guid_id { get; set; }
        public string master_id { get; set; }
        public string parent_id { get; set; }
        public string name { get; set; }
        public string topic { get; set; }
        public int type { get; set; }
        public int level { get; set; }
        public int slow_mode { get; set; }
        public int limit_aamount { get; set; }
        public bool is_category { get; set; }
        public string server_url { get; set; }
    }
}
