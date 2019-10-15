using Ruanmou04.EFCore.Dtos.ForumDtos;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
{
    public interface IRoleForumApplication : IApplication
    {
        /// <summary>
        /// 获取编辑用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns> 
        public ForumRoleChannelDto GetForumRoleChannelById(int id);

        /// <summary>
        /// 获取已经设置角色频道
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns> 
        public List<ForumRoleChannelDto> GetRoleChannelByRoleId(int roleId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="channelIdIds"></param>
        public void SaveForumRoleChannel(int roleId, string channelIdIds);
    }
}
