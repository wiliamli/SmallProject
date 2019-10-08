using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using System;

namespace Ruanmou04.EFCore.Dtos.DtoHelper
{
    /// <summary>
    /// ajax返回结果
    /// </summary>
    public class LoginResult:AjaxResult
    {
        public string Token { get; set; }
        
        public Guid UserId { get; set; }
        public SysUserOutputDto User { get; set; }
        //public LoginResultType LoginResultType { get; set; }
    }
}
