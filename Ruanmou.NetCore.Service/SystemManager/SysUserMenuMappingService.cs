

using Microsoft.EntityFrameworkCore;
using Ruanmou04.NetCore.Interface.SystemManager.Service;

namespace Ruanmou.NetCore.Service
{
    public class SysUserMenuMappingService : BaseService, ISysUserMenuMappingService
    {
        public SysUserMenuMappingService(DbContext context) : base(context)
        {

        }

        //public List<SysUserMenuMapping> GetUserMenu(int userId)
        //{
        //    base.Query<SysUserMenuMapping>
        //}
        


    }
}
