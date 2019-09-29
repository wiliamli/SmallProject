using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos
{
   public class SysMenuDto: BaseDto
    {
        /// <summary>
        /// 上级菜单：根目录id为0
        /// <summary>
        public int? ParentId { get; set; }
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
        /// <summary>
        /// 排序值
        /// <summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 状态：0 正常 1 冻结 2 删除
        /// <summary>
        public bool Status { get; set; } = true;
        /// <summary>
        /// 添加时间
        /// <summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 添加用户
        /// <summary>
        public int? CreatorId { get; set; }
        /// <summary>
        /// 修改时间
        /// <summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 修改用户
        /// <summary>
        public int? LastModifierId { get; set; }
    }
}
