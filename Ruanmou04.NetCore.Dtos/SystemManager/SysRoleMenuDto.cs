using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
    public class SysRoleMenuDto : BaseEntity
    {
        /// <summary>
        /// 角色Id
        /// <summary>
        public int? SysRoleId { get; set; }
        /// <summary>
        /// 菜单Id
        /// <summary>
        public int? SysMenuId { get; set; }
        /// <summary>
        /// 菜单Id
        /// <summary>
        public string SysMenuIds { get; set; }
    }
}
