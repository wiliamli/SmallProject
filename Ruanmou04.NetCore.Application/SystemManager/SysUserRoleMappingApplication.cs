using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using System;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class SysUserRoleMappingApplication : ISysUserRoleMappingApplication
    {
        private readonly ISysUsersRoleService _sysRoleService;
        public SysUserRoleMappingApplication(ISysUsersRoleService sysRoleService)
        {
            _sysRoleService = sysRoleService;
        }

        public void SaveUserRole(int userId, string roleIds)
        {
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
            _sysRoleService.Commit();
        }
    }
}
