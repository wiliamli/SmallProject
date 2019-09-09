using RM04.DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Models.Forum
{
    public class ForumTopic:BaseEntity
    {
        /// <summary>
        /// 频道Id
        /// </summary>
        public int ChannelId { get; set; }

        /// <summary>
        /// 主题名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主题内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// PV量
        /// </summary>
        public int PV { get; set; }

        /// <summary>
        /// 点赞
        /// </summary>
        public int Conssensus { get; set; }

        /// <summary>
        /// 反对
        /// </summary>
        public int Oppose { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifiedId { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        public string ModifiedBy { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }
}
