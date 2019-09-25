using System;
using System.ComponentModel.DataAnnotations;

namespace Ruanmou04.EFCore.Model.Token.Dtos
{
    /// <summary>
    /// 令牌信息
    /// </summary>
    public partial class TokenInformation
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
		[StringLength(500)]
        public string Token { get; set; }


        /// <summary>
        /// 登录账号
        /// </summary>
		[StringLength(100)]
        public string Account { get; set; }


        /// <summary>
        /// 是否有效
        /// </summary>
		public int? IsEffective { get; set; }


        /// <summary>
        /// 标识过期时间
        /// </summary>
		public DateTime? FailureTime { get; set; }


        /// <summary>
        /// 登录IP
        /// </summary>
		[StringLength(100)]
        public string UserIP { get; set; }


        /// <summary>
        /// 客户端信息
        /// </summary>
		[StringLength(500)]
        public string ClientInformation { get; set; }

    }
}
