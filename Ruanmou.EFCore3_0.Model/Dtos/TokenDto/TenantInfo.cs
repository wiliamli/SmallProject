using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruanmou04.EFCore.Model.Token.Dtos
{
    /// <summary>
    /// 租户信息
    /// </summary>
    [Table("B_TenantInfo")]
    public partial class TenantInfo : Entity<Guid>
    {
         


        /// <summary>
        /// 组织机构ID
        /// </summary>
		public Guid? OrgID { get; set; }


        /// <summary>
        /// 有效期
        /// </summary>
		public DateTime? EffectiveDate { get; set; }


        /// <summary>
        /// 联系人
        /// </summary>
		[StringLength(500)]
		public string Contact { get; set; }


        /// <summary>
        /// 联系电话
        /// </summary>
		[StringLength(100)]
		public string ContactNumber { get; set; }

    }
}
