using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input
{
    public class SysUserAddInputDto : SysUserDto
    {
        public SysUserAddInputDto()
        {
            this.CreateTime = DateTime.Now;
        }
    }
}
