using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
{
    public interface ISysResourceApplication : IApplication
    {
        public void AddResource(SysResourceAddInputDto resourceDto);

        public void EditResource(SysResourceEditInputDto resourceEditDto);

        public SysResourceDto GetResourceById(int id);

        public PagedResult<SysResourceListOutputDto> GetPagedResult(SysResourceListInputDto param);
    }
}
