

using Microsoft.EntityFrameworkCore;
using Ruanmou04.NetCore.Interface.SystemManager.Service;

namespace Ruanmou.NetCore.Service
{
    public class SysUserRoleMappingService : BaseService, ISysUserRoleMappingService
    {
        public SysUserRoleMappingService(DbContext context) : base(context)
        {
        }

        


    }
}
