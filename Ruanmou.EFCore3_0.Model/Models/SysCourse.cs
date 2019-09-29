using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Models
{
    public class SysCourse : BaseEntity
    {
        /// <summary>
        /// 课程分类ID
        /// </summary>
        public int CourseCategoryID { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 缩略图[一张]：保存图片路径值
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 腾讯详情地址：跳转到腾讯课堂的地址 
        /// </summary>
        public string TencentUrl { get; set; }
        /// <summary>
        /// 课程上课时间
        /// </summary>
        public string CourseTime { get; set; }
        /// <summary>
        /// 主讲老师
        /// </summary>
        public string Lecturer { get; set; }
        /// <summary>
        /// 课程概述
        /// </summary>
        public string Brief { get; set; }
        /// <summary>
        /// 详细描述 
        /// </summary>
        public string Details { get; set; }
        /// <summary>
        /// 热门推荐
        /// </summary>
        public int IsHot { get; set; }
        /// <summary>
        /// 课程标签(用'|'分隔)：如 促销|免费|推荐
        /// </summary>
        public string CourseTags { get; set; }
        /// <summary>
        /// 浏览量、点击量
        /// </summary>
        public int BrowseCount { get; set; }
        /// <summary>
        /// 累计评价
        /// </summary>
        public int CommentsCount { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
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
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public int? LastModifierId { get; set; } 
    }
}