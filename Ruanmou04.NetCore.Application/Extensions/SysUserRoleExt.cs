using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Application.Extensions
{
    public static class SysUserRoleExt
    {
        //public static SysUserRoleMapping ToEntity(this SysRoleAddInputDto dto)
        //{
        //    if (dto == null)
        //    {
        //        return null;
        //    }
        //    var result = DataMapping<SysRoleAddInputDto, SysUserRoleMapping>.Trans(dto);
        //    return result;
        //}

        //public static SysUserRoleMapping ToEntity(this SysRoleEditInputDto dto)
        //{
        //    if (dto == null)
        //    {
        //        return null;
        //    }
        //    var result = DataMapping<SysRoleEditInputDto, SysUserRoleMapping>.Trans(dto);
        //    return result;
        //}
         

        public static SysUserRoleDto ToRoleDto(this SysUserRoleMapping entity)
        {
            if (entity == null)
            {
                return null;
            }
            var result = DataMapping<SysUserRoleMapping, SysUserRoleDto>.Trans(entity);
            return result;
        }


        public static List<SysUserRoleDto> ToDtos(this IEnumerable<SysUserRoleMapping> entities)
        {
            if (!entities.HasAny())
            {
                return null;
            }

            var result = new List<SysUserRoleDto>();
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


        //public static PagedResult<SysRoleListOutputDto> ToPaged(this PagedResult<SysRole> param)
        //{
        //    var pagedResult = new PagedResult<SysRoleListOutputDto>();
        //    if (param == null)
        //    {
        //        return pagedResult;
        //    }

        //    pagedResult.Rows = param.Rows.Select(u => DataMapping<SysRole, SysRoleListOutputDto>.Trans(u)).ToList();
        //    pagedResult.PageIndex = param.PageIndex;
        //    pagedResult.PageSize = param.PageSize;
        //    pagedResult.Total = param.Total;
        //    return pagedResult;
        //}
    }
}
