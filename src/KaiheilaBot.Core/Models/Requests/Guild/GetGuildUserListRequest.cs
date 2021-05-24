using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Guild
{
    public record GetGuildUserListRequest : BaseRequestWithPage
    {
        public override Method RequestMethod { get; init; } = Method.GET;

        public override string ResourcePath { get; init; } = "guild/user-list";

        public override int Page { get; set; } = 1;

        public override int PageSize { get; set; } = 100;

        public override string Sort { get; set; } = "";

        /// <summary>
        /// 必需 - 服务器的 ID
        /// </summary>
        [ParameterName("guild_id")]
        public string GuildId { get; set; }

        /// <summary>
        /// 非必需 - 服务器中频道的 ID
        /// </summary>
        [ParameterName("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// 非必需 - 搜索关键字，在用户名或昵称中搜索
        /// </summary>
        [ParameterName("search")]
        public string Search { get; set; }

        /// <summary>
        /// 非必需 - 角色 ID，获取特定角色的用户列表
        /// </summary>
        [ParameterName("role_id")]
        public int RoleId { get; set; }

        /// <summary>
        /// 非必需 - 只能为 0 或 1，0 是未认证，1 是已认证
        /// </summary>
        [ParameterName("mobile_verified")]
        public int MobileVerified { get; set; }

        /// <summary>
        /// 非必需 - 根据活跃时间排序，0 是顺序排列，1 是倒序排列
        /// </summary>
        [ParameterName("active_time")]
        public int ActiveTime { get; set; }

        /// <summary>
        /// 非必需 - 根据加入时间排序，0 是顺序排列，1 是倒序排列
        /// </summary>
        [ParameterName("joined_at")]
        public int JoinedAt { get; set; }
    }
}