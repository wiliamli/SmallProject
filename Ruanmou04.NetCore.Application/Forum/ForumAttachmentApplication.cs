using Ruanmou04.EFCore.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.NetCore.Application
{
    public class ForumAttachmentApplication : IForumAttachmentApplication
    {
        public IForumAttachmentService forumAttachmentService;

        public ForumAttachmentApplication(IForumAttachmentService forumAttachmentService)
        {
            this.forumAttachmentService = forumAttachmentService;
        }

        public int AddForumAttachment(ForumAttachmentDto forumAttachmentDto)
        {
            var forumAttachment = forumAttachmentService.Insert<ForumAttachment>(forumAttachmentDto.ToEntity());
            return forumAttachment.Id;
        }

        public void DeleteForumAttachment(int id)
        {
            forumAttachmentService.Delete<ForumAttachment>(id);
        }

        public void EditForumAttachment(ForumAttachmentDto forumAttachmentDto)
        {
            forumAttachmentService.Update<ForumAttachment>(forumAttachmentDto.ToEntity());
        }

        public IEnumerable<ForumAttachmentDto> GetForumAttachmentByCreatedId(int createdId)
        {
            return forumAttachmentService.Query<ForumAttachment>(m => m.CreatedId == createdId).ToDtos();
;        }

        public ForumAttachmentDto GetForumAttachmentById(int id)
        {
            return forumAttachmentService.Find<ForumAttachment>(id).ToDto();
        }

        public IEnumerable<ForumAttachmentDto> GetForumAttachmentByTopicId(int topicId)
        {
            return forumAttachmentService.Query<ForumAttachment>(m => m.TopicId == topicId).ToDtos();
        }
    }
}
