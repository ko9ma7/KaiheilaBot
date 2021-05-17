using System.Collections.Generic;

namespace KaiheilaBot
{
    public class NewChannel : Extra
    {
        public string id { get; set; }
        public string name { get; set; }
        public string user_id { get; set; }
        public string guild_id { get; set; }
        public int is_category { get; set; }
        public string parent_id { get; set; }
        public object level { get; set; }
        public int slow_mode { get; set; }
        public string topic { get; set; }
        public int type { get; set; }
        public IList<PermissionOverwrite> permission_overwrites { get; set; }
        public object permission_users { get; set; }
        public int permission_sync { get; set; }

    }
    public class PermissionOverwrite
    {
        public int role_id { get; set; }
        public int allow { get; set; }
        public int deny { get; set; }
    }
}