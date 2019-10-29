using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input
{
   public class UserUpdatePwdInputDto: UserRestPwdInputDto
    {
        /// <summary>
        /// 用户旧密码
        /// </summary>
        public string UserOldPwd { set; get; }
    }
}
