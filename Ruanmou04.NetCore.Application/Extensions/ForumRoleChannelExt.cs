using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Application.Extensions
{
    public static class ForumRoleChannelExt
    {
        public static ForumRoleChannelDto ToDto(this ForumRoleChannel entity)
        {
            if (entity == null)
            {
                return null;
            }
            var result = DataMapping<ForumRoleChannel, ForumRoleChannelDto>.Trans(entity);
            return result;
        }


        public static List<ForumRoleChannelDto> ToDtos(this IEnumerable<ForumRoleChannel> entities)
        {
            if (!entities.HasAny())
            {
                return null;
            }

            var result = new List<ForumRoleChannelDto>();
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
