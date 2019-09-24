using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
    public class SysCourseCategory : BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类描述
        /// </summary>
        public string CateDescription { get; set; }
        /// <summary>
        /// 父级分类ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 分类图标  
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 前台可见：可见(1)，不可见(0)
        /// </summary>
        public int IsVisible { get; set; }
        /// <summary>
        /// 分类路径
        /// </summary>
        public string CategoryPath { get; set; }
        /// <summary>
        /// 是最后节点
        /// </summary>
        public int IsLastNode { get; set; }
        /// <summary>
        /// 子分类数量
        /// </summary>
        public int ChildCount { get; set; }
        /// <summary>
        /// 课程数量
        /// </summary>
        public int CourseCount { get; set; }
        /// <summary>
        /// 备注 
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateId { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime LastModifyTime { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public int LastModifierId { get; set; }
    }
}