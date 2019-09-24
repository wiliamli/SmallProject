
using RM04.DBEntity;
using Ruanmou.EFCore3_0.Model;
using Ruanmou04.NetCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Application
{
    public interface ISysUserApplication : IApplication
    {
        
        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="user"></param>

        void UpdateLastLogin(SysUser user);
    }
}
