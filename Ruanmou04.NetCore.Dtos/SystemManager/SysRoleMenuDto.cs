using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager
{
    public class SysRoleMenuDto : BaseDto
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
