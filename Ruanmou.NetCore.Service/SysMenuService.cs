

using Microsoft.EntityFrameworkCore;
using RM04.DBEntity;
using Ruanmou.EFCore3_0.Model;
using Ruanmou.NetCore.Interface;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Service
{
    public class SysMenuService : BaseService, ISysMenuService
    {
        public SysMenuService(DbContext context) : base(context)
        {
        }

        


    }
}
