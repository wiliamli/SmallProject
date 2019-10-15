using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Input
{
    public class SysMenuAddInputDto : SysMenuDto
    {
        public SysMenuAddInputDto()
        {
            this.CreateTime = DateTime.Now;
        }
    }
}
