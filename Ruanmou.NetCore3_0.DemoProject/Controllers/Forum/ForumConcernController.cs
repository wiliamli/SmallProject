using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.NetCore.Interface.Forum.Applications;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumConcernController : BaseApiController
    {
        private IForumConcernApplication forumConcernApplication;
        private IMemoryCache memoryCache;
        public ForumConcernController(IForumConcernApplication forumConcernApplication, IMemoryCache memoryCache)
            :base(memoryCache)
        {
            this.forumConcernApplication = forumConcernApplication;
        }


        // GET: api/ForumConcern
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // POST: api/ForumConcern
        [HttpPost]
        public StandardJsonResult<int> Add(ForumConcernDto forumConcernDto)
        {
            return StandardAction<int>(()=> forumConcernApplication.AddConcern(forumConcernDto));
        }

        // DELETE: api/ApiWithActions/5
        [HttpGet]
        public StandardJsonResult Delete(int id)
        {
            return StandardAction(() => forumConcernApplication.DeleteConcern(id));
        }
    }
}
