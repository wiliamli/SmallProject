

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
