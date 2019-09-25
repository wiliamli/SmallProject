using System;
using System.ComponentModel.DataAnnotations;

namespace Ruanmou04.EFCore.Model.Token.Dtos
{
    /// <summary>
    /// 令牌信息明细
    /// </summary>
    public partial class TokenInformationDetail
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
		[StringLength(100)]
		public string SystemID { get; set; }


        /// <summary>
        /// 关联的令牌信息
        /// </summary>
		public Guid? TokenInformationID { get; set; }

    }
}
