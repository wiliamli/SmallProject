﻿using System;
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
using Microsoft.Data.SqlClient;
using Ruanmou04.Core.Utility.MvcResult;

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

        public void EditUser4Memeber(SysUserEditInputDto userEditDto)
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
            sysUserEntity.WeChat = userEditDto.WeChat;
            sysUserEntity.LastModifyTime = DateTime.Now;
            sysUserEntity.LastModifyId = userEditDto.LastModifyId;
            sysUserService.Update(sysUserEntity);
        }


        public void RestUserPwd(UserRestPwdInputDto resetUserPwdDto)
        {
            var sysUserEntity = sysUserService.Find<SysUser>(resetUserPwdDto.UserId);
            if (sysUserService == null)
            {
                return;
            }
            sysUserEntity.Password = resetUserPwdDto.UserPwd;
            sysUserEntity.LastModifyTime = DateTime.Now;
            sysUserEntity.LastModifyId = resetUserPwdDto.LastModifyId;
            sysUserService.Update<SysUser>(sysUserEntity);
        }

        public StandardJsonResult UpdateUserPwd(UserUpdatePwdInputDto updateUserPwdDto)
        {
            if (updateUserPwdDto == null || 
                updateUserPwdDto.UserId<=0 ||
                updateUserPwdDto.UserOldPwd.IsNullOrWhiteSpace() ||
                updateUserPwdDto.UserPwd.IsNullOrWhiteSpace())
            {
                return StandardJsonResult.GetFailureResult("参数有误");
            }
            var sysUserEntity = sysUserService.Find<SysUser>(updateUserPwdDto.UserId);
            if (sysUserEntity == null)
            {
                return StandardJsonResult.GetFailureResult("请重新登录后修改");
            }
            if (!updateUserPwdDto.UserOldPwd.Equals(sysUserEntity.Password))
            {
                return StandardJsonResult.GetFailureResult("请输入正确的原始密码");
            }

            sysUserEntity.Password = updateUserPwdDto.UserPwd;
            sysUserEntity.LastModifyTime = DateTime.Now;
            sysUserEntity.LastModifyId = updateUserPwdDto.LastModifyId;
            sysUserService.Update<SysUser>(sysUserEntity);

            return StandardJsonResult.GetSuccessResult("操作成功");
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
            if (userIds.IsNullOrWhiteSpace())
            {
                return;
            }
            var userIdArray = userIds.Split(',');
            if (!userIdArray.HasAny())
            {
                return;
            }

            var paramList = new List<SqlParameter[]>();          
            foreach (var item in userIdArray)
            {
                paramList.Add(new SqlParameter[2]{
                                                  new SqlParameter("@status", status) ,
                                                  new SqlParameter("@userIds", item)
                });
            }   
            sysUserService.Excute<SysUser>($"UPDATE [SystemUser] SET Status=@status WHERE Id = (@userIds)", paramList);
        }
    }
}
