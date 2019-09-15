using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RM04.DBEntity
{
    public class SysUserInputDto: BaseEntity
    {
        /// <summary>
        /// 用户名
        /// <summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户状态   1 正常 0 删除
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
        public Int64 Mobile { get; set; }
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
        public Int64 QQ { get; set; }
        /// <summary>
        /// 微信号
        /// <summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 性别 男 女
        /// <summary>
        public string Sex { get; set; }
    }
}