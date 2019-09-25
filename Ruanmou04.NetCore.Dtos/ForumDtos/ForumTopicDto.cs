using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility;
using Ruanmou04.EFCore.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.EFCore.Dtos.ForumDtos
{
    public class ForumTopicDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 频道Id
        /// </summary>
        public int ChannelId { get; set; }

        /// <summary>
        /// 主题名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主题内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// PV量
        /// </summary>
        public int PV { get; set; }

        /// <summary>
        /// 点赞
        /// </summary>
        public int Conssensus { get; set; }

        /// <summary>
        /// 反对
        /// </summary>
        public int Oppose { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifiedId { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        public string ModifiedBy { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }

    public static class ForumTopicDtoExt
    {
        public static ForumTopicDto ToDto(this ForumTopic forumTopic)
        {
            ForumTopicDto dto = null;
            if (forumTopic != null)
            {
                dto = DataMapping<ForumTopic, ForumTopicDto>.Trans(forumTopic);
            }
            return dto;
        }

        public static IEnumerable<ForumTopicDto> ToDtos(this IEnumerable<ForumTopic> forumTopics)
        {
            IEnumerable<ForumTopicDto> dtos = null;
            if (forumTopics != null)
            {
                dtos = forumTopics.Select(m => DataMapping<ForumTopic, ForumTopicDto>.Trans(m));
            }
            return dtos;
        }

        public static PagedResult<ForumTopicDto> ToPaged(this PagedResult<ForumTopic> forumTopics)
        {
            PagedResult<ForumTopicDto> pagedResult = new PagedResult<ForumTopicDto>();
            if (forumTopics != null)
            {
                pagedResult.Rows = forumTopics.Rows.Select(m => DataMapping<ForumTopic, ForumTopicDto>.Trans(m)).ToList();
                pagedResult.PageIndex = forumTopics.PageIndex;
                pagedResult.PageSize = forumTopics.PageSize;
                pagedResult.Total = forumTopics.Total;
            }
            return pagedResult;
        }

        public static ForumTopic ToEntity(this ForumTopicDto dto)
        {
            ForumTopic forumTopic = null;
            if (dto != null)
            {
                forumTopic = DataMapping<ForumTopicDto, ForumTopic>.Trans(dto);
            }
            return forumTopic;
        }
    }
}
