using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruanmou04.EFCore.Model.Token.Dtos
{
    /// <summary>
    /// 令牌信息明细
    /// </summary>
    [Table("B_TokenInformationDetail")]
    public partial class TokenInformationDetail : Entity<Guid>
    {

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
