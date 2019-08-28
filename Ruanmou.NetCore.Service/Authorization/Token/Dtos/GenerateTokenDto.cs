using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Service.Core.Tokens.Dtos
{
    /// <summary>
    /// 生成tokenDto
    /// </summary>
    public class GenerateTokenDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan TokenExpiration { get; set; }

    }
}
