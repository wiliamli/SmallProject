using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.ForumDtos;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Project.Controllers;
using System.Collections.Generic;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    [ServiceFilter(typeof(SysVerifyAttribute))]
    [Route("api/[controller]/[action]"), ApiController]
    public class RoleForumController : BaseApiController // ControllerBase
    {
        private readonly IRoleForumApplication _roleForumApplication;

        public RoleForumController(ICurrentUserInfo currentUserInfo, IRoleForumApplication roleForumApplication) : base(currentUserInfo)
        {
            _roleForumApplication = roleForumApplication;
        }

        /// <summary>
        /// 获取编辑用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<ForumRoleChannelDto> GetEditChannelByRoleID(int roleId)
        {
            return StandardAction(() => _roleForumApplication.GetForumRoleChannelById(roleId));
        }

        /// <summary>
        /// 获取已经设置角色频道
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<List<ForumRoleChannelDto>> GetRoleChannelByRoleID(int roleId)
        {
            return StandardAction(() => _roleForumApplication.GetRoleChannelByRoleId(roleId));
        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult SaveData([FromBody]ForumRoleChannelDto sysMenuDto)
        {
            if (sysMenuDto==null)
            {
                StandardJsonResult.GetFailureResult("参数有误");
            }
            return StandardAction(() => _roleForumApplication.SaveForumRoleChannel(sysMenuDto.SysRoleId, sysMenuDto.ForumIds));
        }
    }
}
