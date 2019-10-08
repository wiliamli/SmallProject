using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Models.SystemManager
{
       
    public class SysMenuOperation : BaseEntity
    {
        public string OperateName { get; set; }
        public string OperateType { get; set; }
        /// <summary>
        /// 状态：0  正常  1 冻结  2 删除
        /// <summary>
        public int Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? CreateId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public int? LastModifierId { get; set; }
    }
}