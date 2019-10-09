using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;   
using Ruanmou04.Core.Dtos.DtoHelper;
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
    public class SysRoleMenuController : ControllerBase
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ISysRoleMenuMappingService _sysRoleService;

        public SysRoleMenuController(ICurrentUserInfo currentUserInfo, ISysRoleMenuMappingService sysRoleService)
        {
            _currentUserInfo = currentUserInfo;
            _sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 获取编辑角色菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetEditRoleMenuByRoleID(int id)
        {
            var user = _sysRoleService.Find<SysRoleMenuMapping>(id)?.MapTo<SysRoleMenuMapping, SysRoleMenuDto>();
            return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });

        }

        /// <summary>
        /// 根据角色Id获取已授权的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetRoleMenuByRoleID(int roleId)
        {
            var roleMenus = _sysRoleService.Query<SysRoleMenuMapping>(rm => rm.SysRoleId == roleId).Select(rm => rm.SysMenuId);
            return JsonConvert.SerializeObject(new AjaxResult { success = true, data = roleMenus });

        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult SaveData([FromBody]SysRoleMenuDto sysMenuDto)
        {

            AjaxResult ajaxResult = new AjaxResult { success = false };
            if (sysMenuDto != null)
            {
                if (!sysMenuDto.SysMenuIds.IsNullOrWhiteSpace())
                {
                    _sysRoleService.DeleteNotCommit<SysRoleMenuMapping>(m => m.SysRoleId == sysMenuDto.SysRoleId);
                    var menuIdAry = sysMenuDto.SysMenuIds.Split(",");
                    for (int i = 0; i < menuIdAry.Length; i++)
                    {
                        var roleMenu = new SysRoleMenuMapping() { SysRoleId = sysMenuDto.SysRoleId };
                        roleMenu.SysMenuId = Convert.ToInt32(menuIdAry[i]);
                        _sysRoleService.InsertNotCommit<SysRoleMenuMapping>(roleMenu);
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
