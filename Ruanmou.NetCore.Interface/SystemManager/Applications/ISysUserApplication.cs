


using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Output;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
{
    public interface ISysUserApplication : IApplication
    {
        
        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="user"></param>
        SysUserDetailOutputDto GetUserByUserId(int userId);

        List<SysUserDto> GetUsers(Expression<Func<SysUser, bool>> funcWhere);

        PagedResult<SysUserListOutputDto> GetPagedResult(SysUserListInputDto userListInputDto);

        void AddUser(SysUserAddInputDto userDto);

        void EditUser(SysUserEditInputDto userEditDto);

        void DeleteByUserId(int userId);

        void DeleteBatchByUserId(string userIds);

        void UpdateUserStatus(string userIds, int status);

        void UpdateLastLogin(SysUser user);

    }
}
