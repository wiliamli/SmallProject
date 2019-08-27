
using RM04.DBEntity;
using System;

namespace Ruanmou04.EFCore.Model.DtoHelper
{
    /// <summary>
    /// ajax返回结果
    /// </summary>
    public class LoginResult:AjaxResult
    {
        public string Token { get; set; }
        
        public Guid UserId { get; set; }
        public SysUserDto User { get; set; }
        //public LoginResultType LoginResultType { get; set; }
    }
}
