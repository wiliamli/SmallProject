using System;
using System.Collections.Generic;
using System.Text;
namespace Ruanmou04.EFCore.Model.Models.SystemManager
{

    public class SysUserMenuMapping : BaseEntity
    {
        /// <summary>
        /// ÓÃ»§Id
        /// </summary>
        public int? SysUserId { get; set; }

        /// <summary>
        ///²Ëµ¥Id 
        /// </summary>
        public int? SysMenuId { get; set; }
    }
}