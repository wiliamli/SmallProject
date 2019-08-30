using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{

    public class CurrentUser
    {
        /// <summary>
        /// 用户名
        /// <summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// <summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户状态   0 正常 1 冻结 2 删除
        /// <summary>
        /// <summary>
        public bool Status { get; set; }
        /// <summary>
        /// 联系电话
        /// <summary>
        public string Phone { get; set; }
        /// <summary>
        /// 手机号
        /// <summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 联系地址
        /// <summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系邮箱
        /// <summary>
        public string Email { get; set; }
        /// <summary>
        /// 联系QQ
        /// <summary>
        public Int64? QQ { get; set; }
        /// <summary>
        /// 微信号
        /// <summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 性别 男:Male 女：Female
        /// <summary>
        public string Sex { get; set; }
        /// <summary>
        /// 最后登陆时间
        /// <summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 添加时间
        /// <summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 添加用户
        /// <summary>
        public int? CreateId { get; set; }
        /// <summary>
        /// 修改时间
        /// <summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 修改用户
        /// <summary>
        public int? LastModifyId { get; set; }
    }
}