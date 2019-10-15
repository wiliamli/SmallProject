using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Application.Extensions;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Output;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using System.Linq;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class SysResourceApplication : ISysResourceApplication
    {
        public ISysResourceService _sysResourceService;

        public SysResourceApplication(ISysResourceService sysResourceService)
        {
            this._sysResourceService = sysResourceService;
        }

        public void AddResource(SysResourceAddInputDto resourceDto)
        {
            _sysResourceService.Insert<SysResource>(resourceDto.ToEntity());
        }

        public void EditResource(SysResourceEditInputDto resourceEditDto)
        {
            var sysResourceEntity = _sysResourceService.Find<SysResource>(resourceEditDto.Id);
            if (sysResourceEntity == null)
            {
                return;
            }

            sysResourceEntity.Name = resourceEditDto.Name;
            sysResourceEntity.Classes = resourceEditDto.Classes;
            sysResourceEntity.Content = resourceEditDto.Content;
            sysResourceEntity.LastModifyTime = resourceEditDto.LastModifyTime;
            sysResourceEntity.LastModifierId = resourceEditDto.LastModifierId;
            _sysResourceService.Update(sysResourceEntity);
        }

        public SysResourceDto GetResourceById(int id)
        {
            return _sysResourceService.Find<SysResource>(id).ToDto();
        }

        public PagedResult<SysResourceListOutputDto> GetPagedResult(SysResourceListInputDto param)
        {
            if (param == null)
            {
                return null;
            }
            var name = param.Name;

            var userData =
             (from r in _sysResourceService.Query<SysResource>(u => (!name.IsNullOrEmpty() && u.Name.Contains(name)) || name.IsNullOrEmpty())
              join ul in _sysResourceService.Query<SysUser>() on r.LastModifierId equals ul.Id into ul
              from ulc in ul.DefaultIfEmpty()
              join uc in _sysResourceService.Query<SysUser>() on r.CreatorId equals uc.Id into uc
              from ucc in uc.DefaultIfEmpty()
              select new SysResourceListOutputDto
              {
                  Id = r.Id,
                  Name = r.Name,
                  Classes = r.Classes,
                  BrowseCount = r.BrowseCount,
                  Content = r.Content,
                  LastModifyTime = r.LastModifyTime,
                  LastModifier = ulc.Name,
                  Creator = ucc.Name
              }).ToList();

            var pagedResult = new PagedResult<SysResourceListOutputDto> { PageIndex = param.PageIndex, PageSize = param.PageSize, Rows = userData, Total = userData.Count };
            return pagedResult;
        }
    }
}
