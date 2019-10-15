using Ruanmou04.Core.Utility.DtoUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Input
{
    public class SysMenuListInputDto : PagingInput
    {
        /// <summary>
        /// 菜单名字
        /// </summary>
        public string Name { set; get; }


    }
}
