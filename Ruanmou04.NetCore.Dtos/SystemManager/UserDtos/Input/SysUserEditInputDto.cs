using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input
{
    public class SysUserEditInputDto:BaseDto
    {   
        /// <summary>
        /// 用户名/姓名
        /// <summary>
        public string Name { get; set; }
     
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
        public long? Mobile { get; set; }


        /// <summary>
        /// 联系地址
        /// <summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系邮箱
        /// <summary>
        /// <summary>
        public string Email { get; set; }

        /// <summary>
        /// 联系QQ
        /// <summary>
        public long? QQ { get; set; }

        /// <summary>
        /// 微信号
        /// <summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 性别 男 女
        /// <summary>
        public string Sex { get; set; }
       
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
