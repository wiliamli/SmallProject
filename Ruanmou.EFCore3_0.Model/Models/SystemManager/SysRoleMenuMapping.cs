using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Models.SystemManager
{
    public class SysRoleMenuMapping : BaseEntity
    {
        /// <summary>
        /// 角色Id
        /// <summary>
        public int? SysRoleId { get; set; }
        /// <summary>
        /// 菜单Id
        /// <summary>
        public int? SysMenuId { get; set; }
    }
}
