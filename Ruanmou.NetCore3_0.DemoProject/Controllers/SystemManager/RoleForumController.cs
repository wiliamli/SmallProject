using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.Forum.Service;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Project.Controllers;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class RoleForumController : BaseApiController // ControllerBase
    {
        //private readonly ICurrentUserInfo _currentUserInfo;
        //private readonly IForumRoleChannelService _sysRoleService;

        private readonly IRoleForumApplication _roleForumApplication;

        public RoleForumController(ICurrentUserInfo currentUserInfo, IRoleForumApplication roleForumApplication) : base(currentUserInfo)
        {
            //_currentUserInfo = currentUserInfo;
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
            //var user = _sysRoleService.Find<ForumRoleChannel>(roleId)?.MapTo<ForumRoleChannel, ForumRoleChannelDto>();
            //return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });

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
            //var roleMenus = _sysRoleService.Query<ForumRoleChannel>(rm => rm.SysRoleId == roleId).Select(rm => rm.ChannelId);
            //return JsonConvert.SerializeObject(new AjaxResult { success = true, data = roleMenus });
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
            //AjaxResult ajaxResult = new AjaxResult { success = false };
            //if (sysMenuDto != null)
            //{
            //    if (!sysMenuDto.ForumIds.IsNullOrWhiteSpace())
            //    {
            //        _sysRoleService.DeleteNotCommit<ForumRoleChannel>(m => m.SysRoleId == sysMenuDto.SysRoleId);
            //        var menuIdAry = sysMenuDto.ForumIds.Split(",");
            //        for (int i = 0; i < menuIdAry.Length; i++)
            //        {
            //            var roleMenu = new ForumRoleChannel() { SysRoleId = sysMenuDto.SysRoleId };
            //            roleMenu.ChannelId = Convert.ToInt32(menuIdAry[i]);
            //            _sysRoleService.InsertNotCommit<ForumRoleChannel>(roleMenu);
            //        }
            //        _sysRoleService.Commit();
            //    }
            //    ajaxResult.msg = "保存成功";
            //    ajaxResult.success = true;
            //}
            //else
            //    ajaxResult.msg = "保存失败";
            //return ajaxResult;
        }
    }
}
