

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
    public class LoginService : BaseService, ISysUserService
    {
        public LoginService(DbContext context) : base(context)
        {
        }

        public void UpdateLastLogin(SysUser user)
        {
            SysUser userDB = base.Find<SysUser>(user.Id);
            userDB.LastLoginTime = DateTime.Now;
            this.Commit();
        }


    }
}
