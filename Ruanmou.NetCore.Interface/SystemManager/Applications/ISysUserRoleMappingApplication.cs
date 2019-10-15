using Ruanmou04.NetCore.Dtos.SystemManager;
using Ruanmou04.NetCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
{
    public interface ISysUserRoleMappingApplication : IApplication
    {

        void SaveUserRole(int userId, string roleIds);

        void SaveRoleUser(int roleId, string userIds);

        public List<SysUserRoleDto> GetUserRoleByRoleId(int roleId);


        public List<SysUserRoleDto> GetUserRoleByUserId(int userId);
    }
}
