using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.Core.Utility;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Project.Models;
using Ruanmou04.NetCore.Project.Utility;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ISysUsersRoleService _sysRoleService;

        public UserRoleController(ICurrentUserInfo currentUserInfo, ISysUsersRoleService sysRoleService)
        {
            _currentUserInfo = currentUserInfo;
            _sysRoleService = sysRoleService;
        }
        [HttpGet]
        public AjaxResult SaveData(int userId, string roleIds)
        {

            AjaxResult ajaxResult = new AjaxResult { success = false };
            _sysRoleService.Delete<SysUserRoleMapping>(ur => ur.SysUserId == userId);
            var roleIdAry = roleIds.Split(",");
            for (int i = 0; i < roleIdAry.Length; i++)
            {
                var model = new SysUserRoleMapping() { SysUserId = userId, SysRoleId = Convert.ToInt32(roleIdAry[i]) };
                _sysRoleService.Insert<SysUserRoleMapping>(model);
            }
            ajaxResult.msg = "保存成功";
            ajaxResult.success = true;

            return ajaxResult;
        }
    }
}
