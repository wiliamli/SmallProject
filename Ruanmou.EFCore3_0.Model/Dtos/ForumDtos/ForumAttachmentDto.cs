using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.Core.Utility;
using Ruanmou04.EFCore.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruanmou04.EFCore.Model.Dtos.ForumDtos
{
    public class ForumAttachmentDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 主题ID
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }

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
        public DateTime? CreateDate { get; set; } = DateTime.Now;

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

    public static class ForumAttachmentDtoExt
    {
        public static ForumAttachmentDto ToDto(this ForumAttachment forumAttachment)
        {
            ForumAttachmentDto dto = null;
            if (forumAttachment != null)
            {
                dto = DataMapping<ForumAttachment, ForumAttachmentDto>.Trans(forumAttachment);
            }
            return dto;
        }

        public static IEnumerable<ForumAttachmentDto> ToDtos(this IEnumerable<ForumAttachment> forumAttachments)
        {
            IEnumerable<ForumAttachmentDto> dtos = null;
            if (forumAttachments != null)
            {
                dtos = forumAttachments.Select(m => DataMapping<ForumAttachment, ForumAttachmentDto>.Trans(m));
            }
            return dtos;
        }

        public static ForumAttachment ToEntity(this ForumAttachmentDto dto)
        {
            ForumAttachment forumAttachment = null;
            if (dto != null)
            {
                forumAttachment = DataMapping<ForumAttachmentDto, ForumAttachment>.Trans(dto);
            }
            return forumAttachment;
        }
    }
}
