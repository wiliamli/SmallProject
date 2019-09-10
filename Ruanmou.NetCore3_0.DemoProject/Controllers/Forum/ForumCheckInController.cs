using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.NetCore.Interface.Forum.Applications;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumCheckInController : BaseApiController
    {
        private IForumCheckInApplication forumCheckInApplication;
        private IMemoryCache memoryCache;

        public ForumCheckInController(IForumCheckInApplication forumCheckInApplication,
            IMemoryCache memoryCache):base(memoryCache)
        {
            this.forumCheckInApplication = forumCheckInApplication;
        }

        // GET: api/ForumCheckIn
        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumCheckInDto>> GetCheckIn(int userId)
        {
            return StandardAction(()=> forumCheckInApplication.getInfoByUserId(userId));
        }

        // POST: api/ForumCheckIn
        [HttpPost]
        public StandardJsonResult<int> Add(ForumCheckInDto forumCheckInDto)
        {
            return StandardAction(() => forumCheckInApplication.AddCheckIn(forumCheckInDto));
        }
    }
}
