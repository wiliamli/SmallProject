using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.EFCore.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.EFCore.Model.Dtos.ForumDtos
{
    public class ForumInvitationDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 主题ID
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// 帖子父ID
        /// </summary>
        public int ParantId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否采纳
        /// </summary>
        public bool Accept { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int Conssensus { get; set; }

        /// <summary>
        /// 反对数
        /// </summary>
        public int Oppose { get; set; }

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

        /// <summary>
        /// 帖子盖楼
        /// </summary>
        public List<ForumInvitationDto> ChildInvitation { get; set; }

    }

    public static class ForumInvitationDtoExt
    {
        public static ForumInvitationDto ToDto(this ForumInvitation forumInvitation)
        {
            ForumInvitationDto dto = null;
            if (forumInvitation != null)
            {
                dto = DataMapping<ForumInvitation, ForumInvitationDto>.Trans(forumInvitation);
            }
            return dto;
        }

        public static IEnumerable<ForumInvitationDto> ToDtos(this IEnumerable<ForumInvitation> forumInvitations)
        {
            IEnumerable<ForumInvitationDto> dtos = null;
            if (forumInvitations != null)
            {
                dtos = forumInvitations.Select(m => DataMapping<ForumInvitation, ForumInvitationDto>.Trans(m));
            }
            return dtos;
        }

        public static ForumInvitation ToEntity(this ForumInvitationDto dto)
        {
            ForumInvitation forumInvitations = null;
            if (dto != null)
            {
                forumInvitations = DataMapping<ForumInvitationDto, ForumInvitation>.Trans(dto);
            }
            return forumInvitations;
        }
    }
}
