using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
    public class SysUserRoleMapping : BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// <summary>
        public int? SysUserId { get; set; }
        /// <summary>
        /// 角色Id
        /// <summary>
        public int? SysRoleId { get; set; }
    }
}