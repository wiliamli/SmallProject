using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
    public class SysUserRoleDto : BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// <summary>
        public int? SysUserId { get; set; }
        /// <summary>
        /// 角色Id
        /// <summary>
        public int? SysRoleId { get; set; }

        /// <summary>
        /// 用户Ids
        /// <summary>
        public string UserIds { get; set; }
    }
}