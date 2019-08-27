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
        /// 用户类型
        /// </summary>
        public Guid UserType { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TenantId { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan TokenExpiration { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        //public Guid HasTen { get; set; }
    }
}
