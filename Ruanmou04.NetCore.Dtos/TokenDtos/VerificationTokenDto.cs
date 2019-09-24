using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou04.NetCore.Service.Core.Tokens.Dtos
{
    public class VerificationTokenDto
    {
        public string Account { get; set; }

        public string SystemID { get; set; }

        public string Token { get; set; }
    }


    /// <summary>
    /// Token是否有效
    /// </summary>
    public enum IsEffective
    {
        Normal = 0, //正常
        BeOverdue = 1, //过期
        Invalid //失效
    }
}
