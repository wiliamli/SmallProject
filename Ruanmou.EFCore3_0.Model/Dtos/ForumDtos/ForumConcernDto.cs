using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.EFCore.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.EFCore.Model.Dtos.ForumDtos
{
    public class ForumConcernDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 关注人
        /// </summary>
        public int ConcernUserId { get; set; }

        /// <summary>
        /// 被关注人
        /// </summary>
        public int AttentionUserId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifiedId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }

    public static class ForumConcernDtoExt
    {
        public static ForumConcernDto ToDto(this ForumConcern forumConcern)
        {
            ForumConcernDto dto = null;
            if (forumConcern != null)
            {
                dto = DataMapping<ForumConcern, ForumConcernDto>.Trans(forumConcern);
            }
            return dto;
        }

        public static IEnumerable<ForumConcernDto> ToDtos(this IEnumerable<ForumConcern> forumConcerns)
        {
            IEnumerable<ForumConcernDto> dtos = null;
            if (forumConcerns != null)
            {
                dtos = forumConcerns.Select(m => DataMapping<ForumConcern, ForumConcernDto>.Trans(m));
            }
            return dtos;
        }

        public static ForumConcern ToEntity(this ForumConcernDto dto)
        {
            ForumConcern forumConcern = null;
            if (dto != null)
            {
                forumConcern = DataMapping<ForumConcernDto, ForumConcern>.Trans(dto);
            }
            return forumConcern;
        }
    }
}
