using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RM04.DBEntity
{
    public class test:BaseEntity
    {
             /// <summary>
             /// 用户名/姓名
             /// <summary>
        public string Name { get; set; }
    }
    [Table("SMUser")]
    public class SysUser : BaseEntity
    {
        /// <summary>
        /// 用户名/姓名
        /// <summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// <summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户状态   0 正常 1 冻结 2 删除
        /// <summary>
        /// <summary>
        public string Status { get; set; }
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
        public string QQ { get; set; }
        /// <summary>
        /// 微信号
        /// <summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 性别 男:Male 女：Female
        /// <summary>
        public string Sex { get; set; }

    }
}