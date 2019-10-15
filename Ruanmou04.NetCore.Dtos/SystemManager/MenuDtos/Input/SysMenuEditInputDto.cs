using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Input
{
    public class SysMenuEditInputDto:BaseDto
    {
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
        /// 菜单图标
        /// <summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 说明
        /// <summary>
        public string Description { get; set; }
  
        /// <summary>
        /// 排序值
        /// <summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 状态：0 正常 1 冻结 2 删除
        /// <summary>
        public bool Status { get; set; } = true;
     
        /// <summary>
        /// 修改时间
        /// <summary>
        public DateTime? LastModifyTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改用户
        /// <summary>
        public int? LastModifierId { get; set; }

    }
}
