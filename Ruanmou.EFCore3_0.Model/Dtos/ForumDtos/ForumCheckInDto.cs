using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.EFCore.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.EFCore.Model.Dtos.ForumDtos
{
    public class ForumCheckInDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 签到用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? CheckDate { get; set; } = DateTime.Now;
    }

    public static class ForumCheckInDtoExt
    {
        public static ForumCheckInDto ToDto(this ForumCheckIn forumCheckIn)
        {
            ForumCheckInDto dto = null;
            if (forumCheckIn != null)
            {
                dto = DataMapping<ForumCheckIn, ForumCheckInDto>.Trans(forumCheckIn);
            }
            return dto;
        }

        public static IEnumerable<ForumCheckInDto> ToDtos(this IEnumerable<ForumCheckIn> forumCheckIns)
        {
            IEnumerable<ForumCheckInDto> dtos = null;
            if (forumCheckIns != null)
            {
                dtos = forumCheckIns.Select(m => DataMapping<ForumCheckIn, ForumCheckInDto>.Trans(m));
            }
            return dtos;
        }

        public static ForumCheckIn ToEntity(this ForumCheckInDto dto)
        {
            ForumCheckIn forumCheckIn = null;
            if (dto != null)
            {
                forumCheckIn = DataMapping<ForumCheckInDto, ForumCheckIn>.Trans(dto);
            }
            return forumCheckIn;
        }
    }
}
