using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ruanmou04.Core.Dtos.DtoHelper;
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
    /// 角色菜单
    /// </summary>
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class SysRoleMenuController : BaseApiController
    {
       // private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ISysRoleMenuMappingApplication _sysRoleMenuMappingApplication;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentUserInfo"></param>
        /// <param name="sysRoleMenuMappingApplication"></param>
        public SysRoleMenuController(ICurrentUserInfo currentUserInfo, ISysRoleMenuMappingApplication sysRoleMenuMappingApplication) : base(currentUserInfo)
        {
           // _currentUserInfo = currentUserInfo;
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
            //var user = _sysRoleService.Find<SysRoleMenuMapping>(id)?.MapTo<SysRoleMenuMapping, SysRoleMenuDto>();
            //return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });
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
            //var roleMenus = _sysRoleService.Query<SysRoleMenuMapping>(rm => rm.SysRoleId == roleId).Select(rm => rm.SysMenuId);
            //return JsonConvert.SerializeObject(new AjaxResult { success = true, data = roleMenus });
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
            //AjaxResult ajaxResult = new AjaxResult { success = false };
            //if (sysMenuDto != null)
            //{
            //    if (!sysMenuDto.SysMenuIds.IsNullOrWhiteSpace())
            //    {
            //        _sysRoleService.DeleteNotCommit<SysRoleMenuMapping>(m => m.SysRoleId == sysMenuDto.SysRoleId);
            //        var menuIdAry = sysMenuDto.SysMenuIds.Split(",");
            //        for (int i = 0; i < menuIdAry.Length; i++)
            //        {
            //            var roleMenu = new SysRoleMenuMapping() { SysRoleId = sysMenuDto.SysRoleId };
            //            roleMenu.SysMenuId = Convert.ToInt32(menuIdAry[i]);
            //            _sysRoleService.InsertNotCommit<SysRoleMenuMapping>(roleMenu);
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
