using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Output;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Application.Extensions;
using Ruanmou04.Core.Utility.Extensions;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class SysUserApplication : ISysUserApplication
    {
        public ISysUserService sysUserService;

        public SysUserApplication(ISysUserService sysUserService)
        {
            this.sysUserService = sysUserService;
        }

        public void AddUser(SysUserAddInputDto userDto)
        {
            sysUserService.Insert<SysUser>(userDto.ToEntity());
        }

        public void DeleteBatchByUserId(string userIds)
        {
            sysUserService.Delete<SysUser>(u => userIds.Contains(u.Id.ToString()));
        }

        public void DeleteByUserId(int userId)
        {
            sysUserService.Delete<SysUser>(userId);
        }

        public void EditUser(SysUserEditInputDto userEditDto)
        {
            var sysUserEntity = sysUserService.Find<SysUser>(userEditDto.Id);
            if (sysUserService == null)
            {
                return;
            }
            sysUserEntity.Mobile = userEditDto.Mobile;
            sysUserEntity.Email = userEditDto.Email;
            sysUserEntity.Name = userEditDto.Name;
            sysUserEntity.Phone = userEditDto.Phone;
            sysUserEntity.QQ = userEditDto.QQ;
            sysUserEntity.Sex = userEditDto.Sex;
            sysUserEntity.Status = userEditDto.Status;
            sysUserEntity.WeChat = userEditDto.WeChat;
            sysUserEntity.Address = userEditDto.Address;
            sysUserEntity.LastModifyTime = DateTime.Now;
            sysUserEntity.LastModifyId = userEditDto.LastModifyId;
            sysUserService.Update<SysUser>(sysUserEntity);
        }

        public PagedResult<SysUserListOutputDto> GetPagedResult(SysUserListInputDto param)
        {
            if (param == null)
            {
                return null;
            }
            var name = param.Name;
            var userType = param.UserType;

            PagedResult<SysUser> pagedResult = sysUserService.QueryPage<SysUser, int>(
               u => ((!name.IsNullOrEmpty() && u.Name.Contains(name)) || name.IsNullOrEmpty()) && (userType == 0 || u.UserType == userType),
               param.PageIndex,
               param.PageSize, n => n.Id, false);

            return pagedResult.ToPaged();

        }

        public SysUserDetailOutputDto GetUserByUserId(int userId)
        {
            var result = sysUserService.Find<SysUser>(userId).ToDetailDto();
            return result;
        }

        public List<SysUserDto> GetUsers(Expression<Func<SysUser, bool>> funcWhere)
        {
            return sysUserService.Query(funcWhere).ToDtos();
        }

        public void UpdateLastLoginDate(int userId)
        {
            var sysUser = sysUserService.Find<SysUser>(userId);
            if (sysUser != null)
            {
                sysUser.LastLoginTime = DateTime.Now;
            }
            sysUserService.Update<SysUser>(sysUser);
        }

        public void UpdateUserStatus(string userIds, int status)
        {

        }
    }
}
