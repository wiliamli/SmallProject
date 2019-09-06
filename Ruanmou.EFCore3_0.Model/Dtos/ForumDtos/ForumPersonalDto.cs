using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.EFCore.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.EFCore.Model.Dtos.ForumDtos
{
    public class ForumPersonalDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifiedId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }

    public static class ForumPersonalDtoExt
    {
        public static ForumPersonalDto ToDto(this ForumPersonal forumPersonal)
        {
            ForumPersonalDto dto = null;
            if (forumPersonal != null)
            {
                dto = DataMapping<ForumPersonal, ForumPersonalDto>.Trans(forumPersonal);
            }
            return dto;
        }

        public static IEnumerable<ForumPersonalDto> ToDtos(this IEnumerable<ForumPersonal> forumPersonals)
        {
            IEnumerable<ForumPersonalDto> dtos = null;
            if (forumPersonals != null)
            {
                dtos = forumPersonals.Select(m => DataMapping<ForumPersonal, ForumPersonalDto>.Trans(m));
            }
            return dtos;
        }

        public static ForumPersonal ToEntity(this ForumPersonalDto dto)
        {
            ForumPersonal forumPersonal = null;
            if (dto != null)
            {
                forumPersonal = DataMapping<ForumPersonalDto, ForumPersonal>.Trans(dto);
            }
            return forumPersonal;
        }
    }
}
