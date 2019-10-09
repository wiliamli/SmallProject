


using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
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
