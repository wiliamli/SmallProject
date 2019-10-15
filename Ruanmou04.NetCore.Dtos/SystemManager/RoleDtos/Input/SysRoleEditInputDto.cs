using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Input
{
    public class SysRoleEditInputDto:BaseDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态：0 正常 1 冻结 2 删除
        /// <summary>
        public bool Status { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public int? LastModifierId { get; set; }

    }
}
