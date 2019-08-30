using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.NetCore.Interface.Forum.Applications;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumAttachmentController : BaseApiController
    {
        private IForumAttachmentApplication forumAttachmentApplication;

        public ForumAttachmentController(IForumAttachmentApplication forumAttachmentApplication)
        {
            this.forumAttachmentApplication = forumAttachmentApplication;
        }

        // GET: api/ForumAttachment
        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumAttachmentDto>> GetForumAttachment(int topicId)
        {
            return StandardAction(() => forumAttachmentApplication.GetForumAttachmentByTopicId(topicId));
        }


        // POST: api/ForumAttachment
        [HttpPost]
        public StandardJsonResult<int> Add(ForumAttachmentDto forumAttachmentDto)
        {
            return StandardAction(() => forumAttachmentApplication.AddForumAttachment(forumAttachmentDto));
        }

        // DELETE: api/ApiWithActions/5
        [HttpGet]
        public StandardJsonResult Delete(int id)
        {
            return StandardAction(() => forumAttachmentApplication.DeleteForumAttachment(id));
        }
    }
}
