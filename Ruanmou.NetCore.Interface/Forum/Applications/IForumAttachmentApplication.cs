using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.Forum.Applications
{
    public interface IForumAttachmentApplication:IApplication
    {
        public int AddForumAttachment(ForumAttachmentDto forumAttachmentDto);

        public void EditForumAttachment(ForumAttachmentDto forumAttachmentDto);

        public void DeleteForumAttachment(int id);

        public ForumAttachmentDto GetForumAttachmentById(int id);

        public IEnumerable<ForumAttachmentDto> GetForumAttachmentByCreatedId(int createdId);
    }
}
