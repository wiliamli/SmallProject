using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
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

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentUserInfo"></param>
        /// <param name="sysUserRoleMappingApplication"></param>
        public UserRoleController(ICurrentUserInfo currentUserInfo, ISysUserRoleMappingApplication sysUserRoleMappingApplication) :base(currentUserInfo)
        {
            _sysUserRoleMappingApplication = sysUserRoleMappingApplication;
        }

        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
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
