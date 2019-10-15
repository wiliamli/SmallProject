using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Output;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.NetCore.Application.Extensions
{
    public static class SysRoleExt
    {
        public static SysRole ToEntity(this SysRoleAddInputDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = DataMapping<SysRoleAddInputDto, SysRole>.Trans(dto);
            return result;
        }

        public static SysRole ToEntity(this SysRoleEditInputDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = DataMapping<SysRoleEditInputDto, SysRole>.Trans(dto);
            return result;
        }
         

        public static SysRoleDto ToRoleDto(this SysRole entity)
        {
            if (entity == null)
            {
                return null;
            }
            var result = DataMapping<SysRole, SysRoleDto>.Trans(entity);
            return result;
        }


        public static List<SysRoleDto> ToDtos(this IEnumerable<SysRole> entities)
        {
            if (!entities.HasAny())
            {
                return null;
            }

            var result = new List<SysRoleDto>();
            foreach (var entity in entities)
            {
                var dto = entity.ToRoleDto();
                if (dto != null)
                {
                    result.Add(dto);
                }
            }
            return result;
        }


        public static PagedResult<SysRoleListOutputDto> ToPaged(this PagedResult<SysRole> param)
        {
            var pagedResult = new PagedResult<SysRoleListOutputDto>();
            if (param == null)
            {
                return pagedResult;
            }

            pagedResult.Rows = param.Rows.Select(u => DataMapping<SysRole, SysRoleListOutputDto>.Trans(u)).ToList();
            pagedResult.PageIndex = param.PageIndex;
            pagedResult.PageSize = param.PageSize;
            pagedResult.Total = param.Total;
            return pagedResult;
        }
    }
}
