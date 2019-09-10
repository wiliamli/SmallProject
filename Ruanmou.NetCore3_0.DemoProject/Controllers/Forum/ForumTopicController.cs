using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Ruanmou04.Core.Utility;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumTopicController : BaseApiController
    {
        private IForumTopicApplication forumTopicApplication;
        private IMemoryCache memoryCache;
        public ForumTopicController(IForumTopicApplication forumTopicApplication,
            IMemoryCache memoryCache):base(memoryCache)
        {
            this.forumTopicApplication = forumTopicApplication;
        }

        // GET: api/ForumTopic
        [HttpPost]
        public StandardJsonResult<PagedResult<ForumTopicDto>> GetForumTopicPager(int channleId, PagingInput pagingInput)
        {
            return StandardAction(()=> forumTopicApplication.GetPagedResult(channleId, pagingInput));
        }


        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumTopicDto>> GetForumTopics(int channleId)
        {
            return StandardAction(() => forumTopicApplication.GetTopicsByChannelId(channleId));
        }

        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumTopicDto>> GetForumTopicsByUserId(int userId)
        {
            return StandardAction(() => forumTopicApplication.GetTopicsByUserId(userId));
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
