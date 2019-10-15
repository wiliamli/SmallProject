using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Input
{
    public class SysResourceEditInputDto:BaseDto
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 班级
        /// </summary>
        public string Classes { get; set; }     

        /// <summary>
        /// 浏览量
        /// </summary>
        public int? BrowseCount { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? PublicTime { get; set; }

        /// <summary>
        /// 修改时间
        /// <summary>
        public DateTime? LastModifyTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改用户
        /// <summary>
        public int LastModifierId { get; set; }

    }
}
