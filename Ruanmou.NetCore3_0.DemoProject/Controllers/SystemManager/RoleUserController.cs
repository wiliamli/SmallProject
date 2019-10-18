using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Dtos.SystemManager;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Project.Controllers;
using System.Collections.Generic;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    /// <summary>
    /// 角色用户
    /// </summary>
    [ServiceFilter(typeof(SysVerifyAttribute))]
    [Route("api/[controller]/[action]"), ApiController]
    public class RoleUserController : BaseApiController
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ISysUserRoleMappingApplication _sysUserRoleMappingApplication;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentUserInfo"></param>
        /// <param name="sysUserRoleMappingApplication"></param>
        public RoleUserController(ICurrentUserInfo currentUserInfo, ISysUserRoleMappingApplication sysUserRoleMappingApplication):base(currentUserInfo)
        {
            _currentUserInfo = currentUserInfo;
            _sysUserRoleMappingApplication = sysUserRoleMappingApplication;
        }

        /// <summary>
        /// 根据角色Id获取已有的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<List<SysUserRoleDto>> GetUserRoleByRoleID(int roleId)
        {
            return StandardAction(() => _sysUserRoleMappingApplication.GetUserRoleByRoleId(roleId));
        }

        /// <summary>
        /// 根据用户Id获取已有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<List<SysUserRoleDto>> GetUserRoleByUserID(int userId)
        {
            return StandardAction(() => _sysUserRoleMappingApplication.GetUserRoleByUserId(userId));
        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult SaveData([FromBody]SysUserRoleDto sysMenuDto)
        {
            return StandardAction(() => _sysUserRoleMappingApplication.SaveRoleUser(sysMenuDto.SysRoleId??0, sysMenuDto.UserIds));
        }
    }
}
