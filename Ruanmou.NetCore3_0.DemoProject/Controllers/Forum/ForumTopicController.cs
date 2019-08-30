using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.NetCore.Interface.Forum.Applications;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumTopicController : BaseApiController
    {
        private IForumTopicApplication forumTopicApplication;

        public ForumTopicController(IForumTopicApplication forumTopicApplication)
        {
            this.forumTopicApplication = forumTopicApplication;
        }

        // GET: api/ForumTopic
        [HttpPost]
        public StandardJsonResult<PagedResult<ForumTopicDto>> GetForumTopic(int channleId, PagingInput pagingInput)
        {
            return StandardAction(()=> forumTopicApplication.GetPagedResult(channleId, pagingInput));
        }

        // GET: api/ForumTopic/5
        [HttpGet]
        public StandardJsonResult<ForumTopicDto> GetTopic(int topicId)
        {
            return StandardAction(() => forumTopicApplication.GetForumTopic(topicId));
        }

        // POST: api/ForumTopic
        [HttpPost]
        public StandardJsonResult<int> Add(ForumTopicDto forumTopicDto)
        {
            return StandardAction(() => forumTopicApplication.AddTopic(forumTopicDto));
        }

        // PUT: api/ForumTopic/5
        [HttpPost]
        public StandardJsonResult Edit(ForumTopicDto forumTopicDto)
        {
           return StandardAction(() => forumTopicApplication.UpdateTopic(forumTopicDto));
        }
    }
}
