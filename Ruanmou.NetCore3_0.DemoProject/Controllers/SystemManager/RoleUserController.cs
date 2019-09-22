using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using Ruanmou.NetCore.Service;
using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.Core.Utility;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.DtoHelper;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Project.Models;

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
        /// 获取编辑用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetUserRoleUserID(int roleId)
        {
            var user = _sysRoleService.Query<SysUserRoleMapping>(ur => ur.SysRoleId == roleId).Select(u => new { userid = u.SysUserId }) ;
            return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });

        }



        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult SaveData([FromBody]SysUserRoleDto sysMenuDto)
        {

            AjaxResult ajaxResult = new AjaxResult { success = false };
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
                ajaxResult.msg = "保存成功";
                ajaxResult.success = true;
            }
            else
                ajaxResult.msg = "保存失败";
            return ajaxResult;
        }
    }
}
