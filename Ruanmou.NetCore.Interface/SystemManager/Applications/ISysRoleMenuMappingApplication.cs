


using Ruanmou04.NetCore.Dtos.SystemManager;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
{
    public interface ISysRoleMenuMappingApplication : IApplication
    {
        SysRoleMenuDto GetRoleMenuById(int id);

        List<SysRoleMenuDto> GetRoleMenuByRoleId(int roleId);

        void SaveRoleMenu(int roleId, string menuIds);
    }
}
