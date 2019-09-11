using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
   public class SysMenuTreeDto: BaseEntity
    {
        /// <summary>
        /// 上级菜单：根目录id为0
        /// <summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 子菜单树
        /// </summary>
        public List<SysMenuTreeDto> ChildrenSysMenuList { get; set; }
        /// <summary>
        /// 菜单名称
        /// <summary>
        public string Text { get; set; }
        /// <summary>
        /// 链接地址
        /// <summary>
        public string Url { get; set; }
        /// <summary>
        /// 菜单等级
        /// <summary>
        public int MenuLevel { get; set; }
        /// <summary>
        /// 类型：1 菜单 2 按钮
        /// <summary>
        public int MenuType { get; set; }
        /// <summary>
        /// 菜单图标
        /// <summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 说明
        /// <summary>
        public string Description { get; set; }
        /// <summary>
        /// 菜单路径：parentpath/guid
        //一级菜单为 root/guid
        /// <summary>
        public string SourcePath { get; set; }

    }
}