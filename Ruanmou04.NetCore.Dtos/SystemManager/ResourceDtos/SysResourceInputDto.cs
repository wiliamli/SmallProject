using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
    public class SysResourceInputDto : BaseEntity
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
        /// 内容
        /// </summary>
        public string Content { get; set; }
        
    }
}
