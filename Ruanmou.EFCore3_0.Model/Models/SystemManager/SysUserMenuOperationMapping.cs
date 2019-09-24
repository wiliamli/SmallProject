using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
    public class SysUserMenuOperationMapping : BaseEntity
    {
        /// <summary>
        /// 角色Id
        /// <summary>
        public int? SysOperationId { get; set; }
        /// <summary>
        /// 菜单Id
        /// <summary>
        public int? SysMenuId { get; set; }

        /// <summary>
        ///用户ID 
        /// </summary>
        public int? SysUserId { get; set; }
    }
}