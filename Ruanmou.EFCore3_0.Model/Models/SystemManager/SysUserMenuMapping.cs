using System;
using System.Collections.Generic;
using System.Text;
namespace Ruanmou04.EFCore.Model.Models.SystemManager
{

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