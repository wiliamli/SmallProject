using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.MenuDtos.Output;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.NetCore.Application.Extensions
{
    public static class SysMenuExt
    {
        public static SysMenu ToEntity(this SysMenuAddInputDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = DataMapping<SysMenuAddInputDto, SysMenu>.Trans(dto);
            return result;
        }

        public static SysMenu ToEntity(this SysMenuEditInputDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = DataMapping<SysMenuEditInputDto, SysMenu>.Trans(dto);
            return result;
        }

        //public static SysUserDetailOutputDto ToDetailDto(this SysMenu entity)
        //{
        //    if (entity == null)
        //    {
        //        return null;
        //    }
        //    var result = DataMapping<SysUser, SysUserDetailOutputDto>.Trans(entity);
        //    return result;
        //}

        public static SysMenuDto ToMenuDto(this SysMenu entity)
        {
            if (entity == null)
            {
                return null;
            }
            var result = DataMapping<SysMenu, SysMenuDto>.Trans(entity);
            return result;
        }


        public static SysMenuTreeDto ToMenuTreeDto(this SysMenu entity)
        {
            if (entity == null)
            {
                return null;
            }
            var result = DataMapping<SysMenu, SysMenuTreeDto>.Trans(entity);
            return result;
        }

        public static List<SysMenuTreeDto> ToDtos(this IEnumerable<SysMenu> entities)
        {
            if (!entities.HasAny())
            {
                return null;
            }

            var result = new List<SysMenuTreeDto>();
            foreach (var entity in entities)
            {
                var dto = entity.ToMenuTreeDto();
                if (dto != null)
                {
                    result.Add(dto);
                }
            }
            return result;
        }


        public static PagedResult<SysMenuListOutputDto> ToPaged(this PagedResult<SysMenu> param)
        {
            PagedResult<SysMenuListOutputDto> pagedResult = new PagedResult<SysMenuListOutputDto>();
            if (param == null)
            {
                return pagedResult;
            }

            pagedResult.Rows = param.Rows.Select(u => DataMapping<SysMenu, SysMenuListOutputDto>.Trans(u)).ToList();
            pagedResult.PageIndex = param.PageIndex;
            pagedResult.PageSize = param.PageSize;
            pagedResult.Total = param.Total;
            return pagedResult;
        }
    }


  
}
