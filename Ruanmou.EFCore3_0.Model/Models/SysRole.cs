 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RM04.DBEntity
{
    [Table("SysRole")]
    public class SysRole : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 状态：0 正常 1 冻结 2 删除
        /// <summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 添加用户
        /// </summary>
        public int? CreateId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public int? LastModifierId { get; set; }
    }
}