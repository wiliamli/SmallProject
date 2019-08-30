 
using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
    public class SysRoleDto : BaseEntity
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

    }
}