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
    /// 角色菜单
    /// </summary>
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class SysRoleMenuController : BaseApiController
    {
        private readonly ISysRoleMenuMappingApplication _sysRoleMenuMappingApplication;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentUserInfo"></param>
        /// <param name="sysRoleMenuMappingApplication"></param>
        public SysRoleMenuController(ICurrentUserInfo currentUserInfo, ISysRoleMenuMappingApplication sysRoleMenuMappingApplication) : base(currentUserInfo)
        {
            _sysRoleMenuMappingApplication = sysRoleMenuMappingApplication;
        }

        /// <summary>
        /// 获取编辑角色菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<SysRoleMenuDto> GetEditRoleMenuByRoleID(int id)
        {
            return StandardAction(() => _sysRoleMenuMappingApplication.GetRoleMenuById(id));
        }

        /// <summary>
        /// 根据角色Id获取已授权的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<List<SysRoleMenuDto>> GetRoleMenuByRoleID(int roleId)
        {
            return StandardAction(() => _sysRoleMenuMappingApplication.GetRoleMenuByRoleId(roleId));
        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult SaveData([FromBody]SysRoleMenuDto sysMenuDto)
        {
            return StandardAction(() => _sysRoleMenuMappingApplication.SaveRoleMenu(sysMenuDto.SysRoleId ?? 0, sysMenuDto.SysMenuIds));
        }
    }
}
