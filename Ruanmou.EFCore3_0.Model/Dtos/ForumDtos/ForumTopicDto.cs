using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.EFCore.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.EFCore.Model.Dtos.ForumDtos
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
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifiedId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedDate { get; set; }
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

        public static IEnumerable<ForumTopicDto> ToDtos(this IEnumerable<ForumTopic> forumCheckIns)
        {
            IEnumerable<ForumTopicDto> dtos = null;
            if (forumCheckIns != null)
            {
                dtos = forumCheckIns.Select(m => DataMapping<ForumTopic, ForumTopicDto>.Trans(m));
            }
            return dtos;
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
