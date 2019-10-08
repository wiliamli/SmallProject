using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;    
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Dtos.SystemManager;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Service;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ISysUserRoleMappingService _sysRoleService;

        public RoleUserController(ICurrentUserInfo currentUserInfo, ISysUserRoleMappingService sysRoleService)
        {
            _currentUserInfo = currentUserInfo;
            _sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 根据角色Id获取已有的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetUserRoleByRoleID(int roleId)
        {
            var user = _sysRoleService.Query<SysUserRoleMapping>(ur => ur.SysRoleId == roleId).Select(u => new { userid = u.SysUserId }) ;
            return JsonConvert.SerializeObject(new AjaxResult { Success = true, data = user });

        }

        /// <summary>
        /// 根据用户Id获取已有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetUserRoleByUserID(int userId)
        {
            var user = _sysRoleService.Query<SysUserRoleMapping>(ur => ur.SysUserId == userId).Select(u =>  u.SysRoleId );
            return JsonConvert.SerializeObject(new AjaxResult { Success = true, data = user });

        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult SaveData([FromBody]SysUserRoleDto sysMenuDto)
        {

            AjaxResult ajaxResult = new AjaxResult { Success = false };
            if (sysMenuDto != null)
            {
                if (!sysMenuDto.UserIds.IsNullOrWhiteSpace())
                {
                    _sysRoleService.DeleteNotCommit<SysUserRoleMapping>(m => m.SysRoleId == sysMenuDto.SysRoleId);
                    var menuIdAry = sysMenuDto.UserIds.Split(",");
                    for (int i = 0; i < menuIdAry.Length; i++)
                    {
                        var roleMenu = new SysUserRoleMapping() { SysRoleId = sysMenuDto.SysRoleId };
                        roleMenu.SysUserId = Convert.ToInt32(menuIdAry[i]);
                        _sysRoleService.InsertNotCommit<SysUserRoleMapping>(roleMenu);
                    }
                    _sysRoleService.Commit();
                }
                ajaxResult.Message = "保存成功";
                ajaxResult.Success = true;
            }
            else
                ajaxResult.Message = "保存失败";
            return ajaxResult;
        }
    }
}
