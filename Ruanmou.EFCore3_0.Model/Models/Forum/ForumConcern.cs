using RM04.DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Models.Forum
{
    public class ForumConcern : BaseEntity
    {
        /// <summary>
        /// 关注人
        /// </summary>
        public int ConcernUserId { get; set; }

        /// <summary>
        /// 被关注人
        /// </summary>
        public int AttentionUserId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifiedId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
