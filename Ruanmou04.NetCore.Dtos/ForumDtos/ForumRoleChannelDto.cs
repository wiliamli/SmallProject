using Ruanmou04.Core.Model.DtoHelper;
using Ruanmou04.EFCore.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.EFCore.Model.Dtos.ForumDtos
{
    public class ForumRoleChannelDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 系统角色Id
        /// </summary>
        public int SysRoleId { get; set; }

        /// <summary>
        /// 频道Id
        /// </summary>
        public int ChannelId { get; set; }

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
        /// 系统角色Id字符串，接收保存返回数据
        /// </summary>
        public string ForumIds { get; set; }
    }

    public static class ForumRoleChannelDtoExt
    {
        public static ForumRoleChannelDto ToDto(this ForumRoleChannel forumRoleChannel)
        {
            ForumRoleChannelDto dto = null;
            if (forumRoleChannel != null)
            {
                dto = DataMapping<ForumRoleChannel, ForumRoleChannelDto>.Trans(forumRoleChannel);
            }
            return dto;
        }

       
        public static ForumRoleChannel ToEntity(this ForumRoleChannelDto dto)
        {
            ForumRoleChannel forumRoleChannel = null;
            if (dto != null)
            {
                forumRoleChannel = DataMapping<ForumRoleChannelDto, ForumRoleChannel>.Trans(dto);
            }
            return forumRoleChannel;
        }
    }
}
