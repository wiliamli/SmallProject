using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;    
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Dtos.SystemManager;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Project.Controllers;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    /// <summary>
    /// 角色用户
    /// </summary>
    [CustomAuthorize]
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
            //var user = _sysRoleService.Query<SysUserRoleMapping>(ur => ur.SysRoleId == roleId).Select(u => new { userid = u.SysUserId }) ;
            //return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });
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
            //var user = _sysRoleService.Query<SysUserRoleMapping>(ur => ur.SysUserId == userId).Select(u =>  u.SysRoleId );
            //return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });
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
            //AjaxResult ajaxResult = new AjaxResult { success = false };
            //if (sysMenuDto != null)
            //{
            //    if (!sysMenuDto.UserIds.IsNullOrWhiteSpace())
            //    {
            //        _sysRoleService.DeleteNotCommit<SysUserRoleMapping>(m => m.SysRoleId == sysMenuDto.SysRoleId);
            //        var menuIdAry = sysMenuDto.UserIds.Split(",");
            //        for (int i = 0; i < menuIdAry.Length; i++)
            //        {
            //            var roleMenu = new SysUserRoleMapping() { SysRoleId = sysMenuDto.SysRoleId };
            //            roleMenu.SysUserId = Convert.ToInt32(menuIdAry[i]);
            //            _sysRoleService.InsertNotCommit<SysUserRoleMapping>(roleMenu);
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
