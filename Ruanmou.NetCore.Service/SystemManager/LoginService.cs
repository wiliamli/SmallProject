﻿

using Aio.Domain.SystemManage.Dtos;
using Microsoft.EntityFrameworkCore;
using RM04.DBEntity;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.Security;
using Ruanmou04.EFCore.Dtos.DtoHelper;

namespace Ruanmou.NetCore.Service
{
    public class LoginService : BaseService, ILoginService
    {
        public LoginService(DbContext context) : base(context)
        {

        }
        public AjaxResult Login(LoginInputDto loginInput)
        {
            AjaxResult ajaxResult = new AjaxResult() { success = false };

            var user = base.Find<SysUser>(u => u.Name == loginInput.Account);
            if (user == null)
            {
                ajaxResult.msg = "用户名或密码不正确,请检查！";
            }
            var password = Encrypt.EncryptionPassword(loginInput.Password);
            if (user.Password != password)
            {
                ajaxResult.msg = "用户名或密码不正确,请检查！";
            }

            else//MapTo
            {
                ajaxResult.data = user.MapTo<SysUser, SysUserOutputDto>();// DataMapping<SysUser, SysUserOutputDto>.Trans(user);
                ajaxResult.success = true;
                ajaxResult.msg = "登录成功";
            }
            return ajaxResult;
        }
    }
}
