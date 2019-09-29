

using Microsoft.EntityFrameworkCore;
using Ruanmou04.NetCore.Interface.SystemManager.Service;

namespace Ruanmou.NetCore.Service
{
    public class SysRoleMenuMappingService : BaseService, ISysRoleMenuMappingService
    {
        public SysRoleMenuMappingService(DbContext context) : base(context)
        {
        }

        


    }
}
