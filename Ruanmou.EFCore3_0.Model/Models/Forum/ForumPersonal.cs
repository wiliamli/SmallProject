
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Models.Forum
{
    public class ForumPersonal:BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

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
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

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
