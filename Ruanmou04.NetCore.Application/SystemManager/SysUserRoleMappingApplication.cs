using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Ruanmou04.NetCore.Application.Extensions;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class SysUserRoleMappingApplication : ISysUserRoleMappingApplication
    {
        private readonly ISysUsersRoleService _sysRoleService;
        public SysUserRoleMappingApplication(ISysUsersRoleService sysRoleService)
        {
            _sysRoleService = sysRoleService;
        }

        public List<SysUserRoleDto> GetUserRoleByRoleId(int roleId)
        {
            var userRoleDtos = _sysRoleService.Query<SysUserRoleMapping>(ur => ur.SysRoleId == roleId).ToDtos();
            return userRoleDtos;
        }

        public List<SysUserRoleDto> GetUserRoleByUserId(int userId)
        {
            var userRoleDtos = _sysRoleService.Query<SysUserRoleMapping>(ur => ur.SysUserId == userId).ToDtos();
            return userRoleDtos;
        }

        public void SaveRoleUser(int roleId, string userIds)
        {
            _sysRoleService.DeleteNotCommit<SysUserRoleMapping>(m => m.SysRoleId == roleId);
            if (!string.IsNullOrEmpty(userIds))
            {
                var menuIdAry = userIds.Split(",");
                for (int i = 0; i < menuIdAry.Length; i++)
                {
                    var roleMenu = new SysUserRoleMapping() { SysRoleId = roleId };
                    roleMenu.SysUserId = Convert.ToInt32(menuIdAry[i]);
                    _sysRoleService.InsertNotCommit(roleMenu);
                }
            }
            _sysRoleService.Commit();
        }

        public void SaveUserRole(int userId, string roleIds)
        {
            _sysRoleService.DeleteNotCommit<SysUserRoleMapping>(ur => ur.SysUserId == userId);
            if (!string.IsNullOrEmpty(roleIds))
            {
                var roleIdAry = roleIds.Split(",");
                for (int i = 0; i < roleIdAry.Length; i++)
                {
                    var model = new SysUserRoleMapping() { SysUserId = userId, SysRoleId = Convert.ToInt32(roleIdAry[i]) };
                    _sysRoleService.InsertNotCommit(model);
                }
            }
            _sysRoleService.Commit();
        }

    }
}
