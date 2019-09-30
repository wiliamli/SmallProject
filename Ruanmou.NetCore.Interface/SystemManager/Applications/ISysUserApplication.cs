


using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Output;
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
        void UpdateLastLoginDate(int userId);

        SysUserDetailOutputDto GetUserByUserId(int userId);

        List<SysUserDto> GetUsers(Expression<Func<SysUser, bool>> funcWhere);

        PagedResult<SysUserListOutput> GetPagedResult(SysUserListInputDto userListInputDto);

        void AddUser(SysUserAddInputDto userDto);

        void EditUser(SysUserEditInputDto userEditDto);

        void DeleteByUserId(int userId);

        void DeleteBatchByUserId(string userIds);
    }
}
