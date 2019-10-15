using Ruanmou04.Core.Utility.DtoUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Input
{
    public class SysRoleListInputDto : PagingInput
    {
        /// <summary>
        /// 角色名字
        /// </summary>
        public string Name { set; get; }
    }
}
