using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Input
{
    public class SysResourceAddInputDto : SysResourceDto
    {
        public SysResourceAddInputDto()
        {
            this.CreateTime = DateTime.Now;
        }
    }
}
