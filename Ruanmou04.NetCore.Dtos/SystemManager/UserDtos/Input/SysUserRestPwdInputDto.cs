using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input
{
    public class UserRestPwdInputDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { set; get; }
    }
}
