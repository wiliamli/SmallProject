using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Models.SystemManager
{
    public class SysResource : BaseEntity
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
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
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
