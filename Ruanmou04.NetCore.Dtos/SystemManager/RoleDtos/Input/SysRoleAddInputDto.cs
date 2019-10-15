using System;

namespace Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Input
{
    public class SysRoleAddInputDto : SysRoleDto
    {
        public SysRoleAddInputDto()
        {
            this.CreateTime = DateTime.Now;
        }
    }
}
