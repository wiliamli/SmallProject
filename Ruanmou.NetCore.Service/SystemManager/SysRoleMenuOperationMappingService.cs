

using Microsoft.EntityFrameworkCore;
using Ruanmou04.NetCore.Interface.SystemManager.Service;

namespace Ruanmou.NetCore.Service
{
    public class SysRoleMenuOperationMappingService : BaseService, ISysRoleMenuOperationMappingService
    {
        public SysRoleMenuOperationMappingService(DbContext context) : base(context)
        {
        }

        


    }
}
