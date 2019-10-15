using Ruanmou.NetCore.Service;
using Ruanmou04.NetCore.Dtos.SystemManager;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using System.Collections.Generic;
using Ruanmou04.NetCore.Application.Extensions;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.Core.Utility.Extensions;
using System;
using Ruanmou04.NetCore.Interface.SystemManager.Service;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class SysRoleMenuMappingApplication : ISysRoleMenuMappingApplication
    {
        private readonly ISysUserMenuMappingService _sysUserMenuMappingService;
        public SysRoleMenuMappingApplication(ISysUserMenuMappingService sysUserMenuMappingService)
        {
            _sysUserMenuMappingService = sysUserMenuMappingService;
        }

        public SysRoleMenuDto GetRoleMenuById(int id)
        {
            var roleMenuDto = _sysUserMenuMappingService.Find<SysRoleMenuMapping>(id).ToDto();
            return roleMenuDto;
        }

        public List<SysRoleMenuDto> GetRoleMenuByRoleId(int roleId)
        {
            var userRoleDtos = _sysUserMenuMappingService.Query<SysRoleMenuMapping>(rm => rm.SysRoleId == roleId).ToDtos();
            return userRoleDtos;
        }

        public void SaveRoleMenu(int roleId, string menuIds)
        {
            _sysUserMenuMappingService.DeleteNotCommit<SysRoleMenuMapping>(m => m.SysRoleId == roleId);
            if (!menuIds.IsNullOrWhiteSpace())
            {
                var menuIdAry = menuIds.Split(",");
                for (int i = 0; i < menuIdAry.Length; i++)
                {
                    var roleMenu = new SysRoleMenuMapping() { SysRoleId = roleId };
                    roleMenu.SysMenuId = Convert.ToInt32(menuIdAry[i]);
                    _sysUserMenuMappingService.InsertNotCommit<SysRoleMenuMapping>(roleMenu);
                }
                _sysUserMenuMappingService.Commit();
            }
        }

    }
}
