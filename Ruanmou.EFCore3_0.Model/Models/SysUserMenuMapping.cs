using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
using System;
    public class SysUserMenuMapping : BaseEntity
    {
        /// <summary>
        /// �û�Id
        /// </summary>
        public int? SysUserId { get; set; }

        /// <summary>
        ///�˵�Id 
        /// </summary>
        public int? SysMenuId { get; set; }
    }
}