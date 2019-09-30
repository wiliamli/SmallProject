using Ruanmou04.Core.Utility.DtoUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input
{
    public class SysUserListInputDto : PagingInput
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { set; get; }
    }
}
