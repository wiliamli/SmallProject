﻿
using RM04.DBEntity;
using Ruanmou.EFCore3_0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Interface
{
    public interface ISysUserService : IBaseService
    {
        
        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="user"></param>

        void UpdateLastLogin(SysUser user);
    }
}
