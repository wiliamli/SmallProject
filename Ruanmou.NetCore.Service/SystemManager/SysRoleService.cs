

using Microsoft.EntityFrameworkCore;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Service
{
    public class SysRoleService : BaseService, ISysUsersRoleService
    {
        public SysRoleService(DbContext context) : base(context)
        {
        }

        


    }
}
