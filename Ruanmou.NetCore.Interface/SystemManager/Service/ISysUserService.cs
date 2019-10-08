using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ruanmou04.NetCore.Interface.SystemManager.Service
{
    public interface ISysUserService : IBaseService
    {
        
        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="user"></param>

        void UpdateLastLogin(SysUser user);

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        List<SysUserOutputDto> GetSysUsers(Expression<Func<SysUser, bool>> funcWhere);
    }
}
