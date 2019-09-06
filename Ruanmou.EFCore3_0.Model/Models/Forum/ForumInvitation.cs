using RM04.DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Models.Forum
{
    /// <summary>
    /// 论坛发帖
    /// </summary>
    public class ForumInvitation:BaseEntity
    {
        /// <summary>
        /// 主题ID
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// 帖子父ID
        /// </summary>
        public int ParantId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否采纳
        /// </summary>
        public bool Accept { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int Conssensus { get; set; }

        /// <summary>
        /// 反对数
        /// </summary>
        public int Oppose { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifiedId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
