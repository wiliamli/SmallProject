using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos;
using Ruanmou04.NetCore.Interface;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
{
    public interface ISysRoleApplication : IApplication
    {
        IEnumerable<SysRoleDto> GetUserRoles(int userId);
    }
}
