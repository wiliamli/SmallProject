using System;
using Microsoft.AspNetCore.Mvc;   
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.AOP.Filter;         
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
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
       // [HttpPost]
        public AjaxResult SaveData(int userId, string roleIds)
        {
            if (userId<=0)
            {
                return AjaxResult.Failure("请求参数有误");
            }
            _sysRoleService.Delete<SysUserRoleMapping>(ur => ur.SysUserId == userId);
            if (!string.IsNullOrEmpty(roleIds))
            {
                var roleIdAry = roleIds.Split(",");
                for (int i = 0; i < roleIdAry.Length; i++)
                {
                    var model = new SysUserRoleMapping() { SysUserId = userId, SysRoleId = Convert.ToInt32(roleIdAry[i]) };
                    _sysRoleService.InsertNotCommit<SysUserRoleMapping>(model);
                }
            }
           // AjaxResult ajaxResult = new AjaxResult { success = false };
           // _sysRoleService.Delete<SysUserRoleMapping>(ur => ur.SysUserId == userId);
          
            _sysRoleService.Commit();
            //ajaxResult.msg = "保存成功";
            //ajaxResult.success = true;

            return AjaxResult.SuccessResult("保存成功");
        }
    }
}
