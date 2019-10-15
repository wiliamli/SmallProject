using Ruanmou04.Core.Utility.DtoUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Input
{
    public class SysResourceListInputDto : PagingInput
    { 
        public string Name { set; get; }
    }
}
