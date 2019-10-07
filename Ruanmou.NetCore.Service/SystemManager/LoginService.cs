using Microsoft.EntityFrameworkCore;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.Security;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.LoginDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Output;
using Ruanmou04.NetCore.Interface.SystemManager.Service;

namespace Ruanmou.NetCore.Service
{
    public class LoginService : BaseService, ILoginService
    {
        public LoginService(DbContext context) : base(context)
        {

        }
        public AjaxResult Login(LoginInputDto loginInput)
        {
            var user = base.Find<SysUser>(u => u.Account == loginInput.Account);
            if (user == null || user.Id <= 0)
            {
                return AjaxResult.Failure("用户名或密码不正确,请检查！");
            }
            var password = Encrypt.EncryptionPassword(loginInput.Password);
            if (user.Password != password)
            {
                return AjaxResult.Failure("用户名或密码不正确,请检查！");
            }

            var data = DataMapping<SysUser, SysUserOutputDto>.Trans(user);
            return AjaxResult.SuccessResult("登录成功", data);
        }
    }
}
