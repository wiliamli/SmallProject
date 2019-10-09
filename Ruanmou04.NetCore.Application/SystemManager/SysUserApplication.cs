using System;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Output;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class SysUserApplication : ISysUserApplication
    {
        public void AddUser(SysUserAddInputDto userDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteBatchByUserId(string userIds)
        {
            throw new NotImplementedException();
        }

        public void DeleteByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void EditUser(SysUserEditInputDto userEditDto)
        {
            throw new NotImplementedException();
        }

        public PagedResult<SysUserListOutputDto> GetPagedResult(SysUserListInputDto userListInputDto)
        {
            throw new NotImplementedException();
        }

        public SysUserDetailOutputDto GetUserByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<SysUserDto> GetUsers(Expression<Func<SysUser, bool>> funcWhere)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="user"></param>

        public void UpdateLastLogin(SysUser user)
        {

        }

        public void UpdateUserStatus(string userIds, int status)
        {
           
        }
    }
}
