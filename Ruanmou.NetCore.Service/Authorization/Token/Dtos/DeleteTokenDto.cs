using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Service.Core.Tokens.Dtos
{
    /// <summary>
    /// 注销令牌Dto
    /// </summary>
    public class LoginOutTokenDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
    }
}
