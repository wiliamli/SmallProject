using System;
using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.AOP.Filter;         
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Project.Controllers;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class UserRoleController :BaseApiController
    {
        private readonly ISysUserRoleMappingApplication _sysUserRoleMappingApplication;

        public UserRoleController(ICurrentUserInfo currentUserInfo, ISysUserRoleMappingApplication sysUserRoleMappingApplication) :base(currentUserInfo)
        {
            _sysUserRoleMappingApplication = sysUserRoleMappingApplication;
        }

        [HttpGet]
        public StandardJsonResult SaveData(int userId, string roleIds)
        {
            if (userId<=0)
            {
                return StandardJsonResult.GetFailureResult("请求参数有误");
            }
            return StandardAction(() => _sysUserRoleMappingApplication.SaveUserRole(userId, roleIds));
        }
    }
}
