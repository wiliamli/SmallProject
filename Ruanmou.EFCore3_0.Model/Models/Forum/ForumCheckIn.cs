using RM04.DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Models.Forum
{
    public class ForumCheckIn : BaseEntity
    {
        /// <summary>
        /// 论坛用户Id
        /// </summary>
        public int UserId { get; set;}


        /// <summary>
        /// 签到日期
        /// </summary>
        public DateTime? CheckDate { get; set; }
    }
}
