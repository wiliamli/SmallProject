using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class SysRoleApplication : ISysRoleApplication
    {
        private ISysUsersRoleService sysRoleService;
        private ISysUserRoleMappingService sysUserRoleMappingService;

        public SysRoleApplication(ISysUsersRoleService sysRoleService,
            ISysUserRoleMappingService sysUserRoleMappingService)
        {
            this.sysRoleService = sysRoleService;
            this.sysUserRoleMappingService = sysUserRoleMappingService;
        }

        public IEnumerable<SysRoleDto> GetUserRoles(int userId)
        {
            IEnumerable<SysRoleDto> sysRoleDtos = null;
            try
            {
                var roleUser = sysUserRoleMappingService.Query<SysUserRoleMapping>(m => m.SysUserId == userId).ToList();

                var roles = sysRoleService.Query<SysRole>(m => m.Status).ToList();

                sysRoleDtos = (from a in roles
                               join b in roleUser on a.Id equals b.SysRoleId
                               select new SysRoleDto()
                               {
                                   Description = a.Description,
                                   Id = a.Id,
                                   Status = a.Status,
                                   Text = a.Text
                               }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sysRoleDtos;
        }
    }
}
