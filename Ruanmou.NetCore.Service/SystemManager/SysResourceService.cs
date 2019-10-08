

using Microsoft.EntityFrameworkCore;
using Ruanmou04.NetCore.Interface.SystemManager.Service;

namespace Ruanmou.NetCore.Service
{
    public class SysResourceService : BaseService, ISysResourceService
    {
        public SysResourceService(DbContext context) : base(context)
        {

        }
    }
}
