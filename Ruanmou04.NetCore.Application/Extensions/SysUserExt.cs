using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.UserDtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruanmou04.NetCore.Application.Extensions
{
    public static class SysUserExt
    {
        public static SysUser ToEntity(this SysUserAddInputDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = DataMapping<SysUserAddInputDto, SysUser>.Trans(dto);
            return result;
        }

        public static SysUser ToEntity(this SysUserEditInputDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = DataMapping<SysUserEditInputDto, SysUser>.Trans(dto);
            return result;
        }

        public static SysUserDetailOutputDto ToDetailDto(this SysUser entity)
        {
            if (entity == null)
            {
                return null;
            }
            var result = DataMapping<SysUser, SysUserDetailOutputDto>.Trans(entity);
            return result;
        }

        public static SysUserDto ToDto(this SysUser entity)
        {
            if (entity == null)
            {
                return null;
            }
            var result = DataMapping<SysUser, SysUserDto>.Trans(entity);
            return result;
        }

        public static List<SysUserDto> ToDtos(this IEnumerable<SysUser> entities)
        {
            if (!entities.HasAny())
            {
                return null;
            }

            var result = new List<SysUserDto>();
            foreach (var entity in entities)
            {
                var dto = entity.ToDto();
                if (dto!=null)
                {
                    result.Add(dto);
                }
            }
            return result;
        }


        public static PagedResult<SysUserListOutput> ToPaged(this PagedResult<SysUser> param)
        {
            PagedResult<SysUserListOutput> pagedResult = new PagedResult<SysUserListOutput>();
            if (param == null)
            {
                return pagedResult;
            }

            pagedResult.Rows = param.Rows.Select(u => DataMapping<SysUser, SysUserListOutput>.Trans(u)).ToList();
            pagedResult.PageIndex = param.PageIndex;
            pagedResult.PageSize = param.PageSize;
            pagedResult.Total = param.Total;
            return pagedResult;
        }
    }


    //public static ForumAttachmentDto ToDto(this ForumAttachment forumAttachment)
    //{
    //    ForumAttachmentDto dto = null;
    //    if (forumAttachment != null)
    //    {
    //        dto = DataMapping<ForumAttachment, ForumAttachmentDto>.Trans(forumAttachment);
    //    }
    //    return dto;
    //}
}
