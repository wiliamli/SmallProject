using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.EFCore.Dtos.ForumDtos
{
    public class ForumChannelDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 频道名称
        /// </summary>
        public string Name { get; set; }

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


        /// <summary>
        /// 对应的主题数据
        /// </summary>
        public IEnumerable<ForumTopicDto> ForumTopics { get; set; }
    }

    public static class ForumChannelDtoExt
    {
        public static ForumChannelDto ToDto(this ForumChannel forumChannel)
        {
            ForumChannelDto dto = null;
            if (forumChannel != null)
            {
                dto = DataMapping<ForumChannel, ForumChannelDto>.Trans(forumChannel);
            }
            return dto;
        }

        public static IEnumerable<ForumChannelDto> ToDtos(this IEnumerable<ForumChannel> forumChannels)
        {
            IEnumerable<ForumChannelDto> dtos = null;
            if (forumChannels != null)
            {
                dtos = forumChannels.Select(m => DataMapping<ForumChannel, ForumChannelDto>.Trans(m));
            }
            return dtos;
        }

        public static ForumChannel ToEntity(this ForumChannelDto dto)
        {
            ForumChannel forumChannel = null;
            if (dto != null)
            {
                forumChannel = DataMapping<ForumChannelDto, ForumChannel>.Trans(dto);
            }
            return forumChannel;
        }
    }
}
