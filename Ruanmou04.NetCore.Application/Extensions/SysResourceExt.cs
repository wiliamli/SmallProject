using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Input;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Application.Extensions
{
    public static class SysResourceExt
    {
        public static SysResource ToEntity(this SysResourceAddInputDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = DataMapping<SysResourceAddInputDto, SysResource>.Trans(dto);
            return result;
        }

        public static SysResource ToEntity(this SysResourceEditInputDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = DataMapping<SysResourceEditInputDto, SysResource>.Trans(dto);
            return result;
        }

        public static SysResourceDto ToDto(this SysResource entity)
        {
            if (entity == null)
            {
                return null;
            }
            var result = DataMapping<SysResource, SysResourceDto>.Trans(entity);
            return result;
        }


        public static List<SysResourceDto> ToDtos(this IEnumerable<SysResource> entities)
        {
            if (!entities.HasAny())
            {
                return null;
            }

            var result = new List<SysResourceDto>();
            foreach (var entity in entities)
            {
                var dto = entity.ToDto();
                if (dto != null)
                {
                    result.Add(dto);
                }
            }
            return result;
        }
    }
}
