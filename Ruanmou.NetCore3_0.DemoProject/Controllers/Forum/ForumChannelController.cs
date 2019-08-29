using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.NetCore.Interface.Forum.Applications;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumChannelController : BaseApiController
    {
        private IForumChannelApplication forumChannelApplication;

        public ForumChannelController(IForumChannelApplication forumChannelApplication)
        {
            this.forumChannelApplication = forumChannelApplication;
        }


        // GET: api/ForumChannel
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ForumChannel/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ForumChannel
        [HttpPost]
        public StandardJsonResult<int> Add(ForumChannelDto forumChannelDto)
        {
            return StandardAction(() => forumChannelApplication.AddForumChannel(forumChannelDto));
        }

        // PUT: api/ForumChannel/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
