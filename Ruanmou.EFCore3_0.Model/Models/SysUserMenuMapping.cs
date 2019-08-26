using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
using System;
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