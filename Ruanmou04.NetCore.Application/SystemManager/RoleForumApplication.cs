using Ruanmou04.EFCore.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.Interface.Forum.Service;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using System;
using System.Collections.Generic;
using System.Text;
using Ruanmou04.NetCore.Application.Extensions;
using Ruanmou04.Core.Utility.Extensions;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class RoleForumApplication : IRoleForumApplication
    {
        private readonly IForumRoleChannelService _forumRoleChannelService;
        public RoleForumApplication(IForumRoleChannelService forumRoleChannelService)
        {
            _forumRoleChannelService = forumRoleChannelService;
        }

        /// <summary>
        /// 获取编辑用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns> 
        public ForumRoleChannelDto GetForumRoleChannelById(int id)
        {
            var result = _forumRoleChannelService.Find<ForumRoleChannel>(id).ToDto();
            return result;
        }

        /// <summary>
        /// 获取已经设置角色频道
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns> 
        public List<ForumRoleChannelDto> GetRoleChannelByRoleId(int roleId)
        {
            var result = _forumRoleChannelService.Query<ForumRoleChannel>(rm => rm.SysRoleId == roleId).ToDtos();
            return result;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="channelIdIds"></param>
        public void SaveForumRoleChannel(int roleId, string channelIdIds)
        {
            _forumRoleChannelService.DeleteNotCommit<ForumRoleChannel>(m => m.SysRoleId == roleId);
            if (!channelIdIds.IsNullOrWhiteSpace())
            { 
                var menuIdAry = channelIdIds.Split(",");
                for (int i = 0; i < menuIdAry.Length; i++)
                {
                    var roleMenu = new ForumRoleChannel() { SysRoleId = roleId };
                    roleMenu.ChannelId = Convert.ToInt32(menuIdAry[i]);
                    _forumRoleChannelService.InsertNotCommit<ForumRoleChannel>(roleMenu);
                }
            }
            _forumRoleChannelService.Commit();
        }
    }
}
